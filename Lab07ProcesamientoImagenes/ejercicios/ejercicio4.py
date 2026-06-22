import cv2
import numpy as np

# cargar una imagen a color
img = cv2.imread('../resultados/1_redimensionada_1.png')

# separar los canales originales
canal_b, canal_g, canal_r = cv2.split(img)

# matriz de ceros del mismo tamaño (canal apagado = negro)
ceros = np.zeros_like(canal_b)

# estado de cada canal: True = visible, False = apagado
ver_r = True
ver_g = True
ver_b = True

print('Controles: r=rojo  g=verde  b=azul  ESC=salir')

while True:
    # si el canal esta apagado, se usa la matriz de ceros
    b = canal_b if ver_b else ceros
    g = canal_g if ver_g else ceros
    r = canal_r if ver_r else ceros

    # recomponer la imagen con los canales activos
    resultado = cv2.merge([b, g, r])
    cv2.imshow('Canales de color (r/g/b alternan, ESC sale)', resultado)

    tecla = cv2.waitKey(1) & 0xFF
    if tecla == ord('r'):
        ver_r = not ver_r
        print(f'Rojo: {"ON" if ver_r else "OFF"}')
    elif tecla == ord('g'):
        ver_g = not ver_g
        print(f'Verde: {"ON" if ver_g else "OFF"}')
    elif tecla == ord('b'):
        ver_b = not ver_b
        print(f'Azul: {"ON" if ver_b else "OFF"}')
    elif tecla == 27:
        break

cv2.destroyAllWindows()