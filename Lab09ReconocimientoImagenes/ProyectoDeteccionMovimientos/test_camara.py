import cv2

cap = cv2.VideoCapture(0)

if not cap.isOpened():
    print("Error: No se pudo acceder a la cámara.")
    exit()

print("Cámara lista. Presiona 'q' para cerrar la ventana.")

while True:
    ret, frame = cap.read()
    if not ret:
        print("Error al recibir los frames.")
        break
        
    cv2.imshow('Prueba Camara - Proyecto', frame)
    
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

cap.release()
cv2.destroyAllWindows()