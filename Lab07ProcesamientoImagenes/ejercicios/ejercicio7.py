import cv2
import numpy as np

# lienzo blanco de 600x800 pixeles
lienzo = np.ones((600, 800, 3), dtype=np.uint8) * 255
historial = []       # pila de estados para deshacer
figura = 'linea'     # figura activa por defecto
color = (0, 0, 255)  # rojo en BGR
dibujando = False
ix, iy = -1, -1      # punto inicial del trazo

def dibujar(evento, x, y, flags, param):
    """callback que responde a eventos del mouse"""
    global ix, iy, dibujando, lienzo

    if evento == cv2.EVENT_LBUTTONDOWN:
        # al hacer click, guardar estado actual y punto inicial
        dibujando = True
        ix, iy = x, y
        historial.append(lienzo.copy())

    elif evento == cv2.EVENT_LBUTTONUP:
        # al soltar, dibujar la figura entre punto inicial y final
        dibujando = False
        if figura == 'linea':
            cv2.line(lienzo, (ix, iy), (x, y), color, 2)
        elif figura == 'rectangulo':
            cv2.rectangle(lienzo, (ix, iy), (x, y), color, 2)
        elif figura == 'circulo':
            radio = int(np.hypot(x - ix, y - iy))
            cv2.circle(lienzo, (ix, iy), radio, color, 2)

# crear ventana y registrar el callback del mouse
cv2.namedWindow('Dibujo interactivo')
cv2.setMouseCallback('Dibujo interactivo', dibujar)

print('Figuras:  l=linea  r=rectangulo  c=circulo')
print('Acciones: z=deshacer  s=guardar  ESC=salir')

while True:
    cv2.imshow('Dibujo interactivo', lienzo)
    tecla = cv2.waitKey(1) & 0xFF

    if tecla == ord('l'):
        figura = 'linea'
        print('Figura: linea')
    elif tecla == ord('r'):
        figura = 'rectangulo'
        print('Figura: rectangulo')
    elif tecla == ord('c'):
        figura = 'circulo'
        print('Figura: circulo')
    elif tecla == ord('z'):
        if historial:
            lienzo = historial.pop()
            print('Deshacer')
        else:
            print('No hay mas cambios para deshacer')
    elif tecla == ord('s'):
        cv2.imwrite('../resultados/7_dibujo_final.png', lienzo)
        print('Guardado como 7_dibujo_final.png')
    elif tecla == 27:
        break

cv2.destroyAllWindows()