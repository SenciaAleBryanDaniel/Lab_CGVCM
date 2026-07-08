import cv2
import mediapipe as mp
import numpy as np

def calcular_angulo(a, b, c):
    a = np.array(a)
    b = np.array(b)
    c = np.array(c)
    
    radianes = np.arctan2(c[1]-b[1], c[0]-b[0]) - np.arctan2(a[1]-b[1], a[0]-b[0])
    angulo = np.abs(radianes * 180.0 / np.pi)
    
    if angulo > 180.0:
        angulo = 360.0 - angulo
        
    return angulo

mp_pose = mp.solutions.pose
mp_drawing = mp.solutions.drawing_utils
pose = mp_pose.Pose(min_detection_confidence=0.5, min_tracking_confidence=0.5)

# Cambiamos el 0 por el 1 para apuntar al periférico USB externo
cap = cv2.VideoCapture(1, cv2.CAP_DSHOW)


# --- VARIABLES DE ESTADO Y CONTEO ---
contador = 0
etapa = None

while True:
    ret, frame = cap.read()
    if not ret:
        break

    frame_rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
    resultado = pose.process(frame_rgb)

    if resultado.pose_landmarks:
        landmarks = resultado.pose_landmarks.landmark
        
        # Extracción de coordenadas de la pierna izquierda
        cadera = [landmarks[mp_pose.PoseLandmark.LEFT_HIP.value].x, 
                  landmarks[mp_pose.PoseLandmark.LEFT_HIP.value].y]
        rodilla = [landmarks[mp_pose.PoseLandmark.LEFT_KNEE.value].x, 
                   landmarks[mp_pose.PoseLandmark.LEFT_KNEE.value].y]
        tobillo = [landmarks[mp_pose.PoseLandmark.LEFT_ANKLE.value].x, 
                   landmarks[mp_pose.PoseLandmark.LEFT_ANKLE.value].y]
        
        angulo = calcular_angulo(cadera, rodilla, tobillo)
        
        # --- LÓGICA DEL CONTADOR DE SENTADILLAS ---
        # Si la persona está de pie (pierna extendida)
        if angulo > 160:
            if etapa == "abajo":
                # Solo suma la repetición si venía de estar agachado
                contador += 1
            etapa = "arriba"
            
        # Si la persona hace la sentadilla (rompe los 90 grados)
        if angulo < 90:
            etapa = "abajo"
            
        # Renderizado del ángulo sobre la rodilla
        h, w, _ = frame.shape
        coordenada_texto = tuple(np.multiply(rodilla, [w, h]).astype(int))
        cv2.putText(frame, str(int(angulo)), coordenada_texto, 
                    cv2.FONT_HERSHEY_SIMPLEX, 1.5, (255, 255, 255), 2, cv2.LINE_AA)

        mp_drawing.draw_landmarks(frame, resultado.pose_landmarks, mp_pose.POSE_CONNECTIONS)

    # --- INTERFAZ GRÁFICA (MARCADOR EN PANTALLA) ---
    cv2.rectangle(frame, (0,0), (250,80), (245,117,16), -1) 
    
    cv2.putText(frame, 'SENTADILLAS', (15,20), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (0,0,0), 1, cv2.LINE_AA)
    cv2.putText(frame, str(contador), (10,65), cv2.FONT_HERSHEY_SIMPLEX, 2, (255,255,255), 2, cv2.LINE_AA)
    
    cv2.putText(frame, 'ESTADO', (130,20), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (0,0,0), 1, cv2.LINE_AA)
    cv2.putText(frame, etapa if etapa else '-', (130,65), cv2.FONT_HERSHEY_SIMPLEX, 1.5, (255,255,255), 2, cv2.LINE_AA)

    cv2.imshow('Proyecto Final - Contador de Sentadillas', frame)

    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

cap.release()
cv2.destroyAllWindows()