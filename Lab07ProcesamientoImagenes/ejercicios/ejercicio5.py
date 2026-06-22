import cv2

# cargar la imagen original (usa una que tenga una cara visible)
foto = cv2.imread('../imagenes/img1.jpg')
gris = cv2.cvtColor(foto, cv2.COLOR_BGR2GRAY)

# cargar el clasificador haar cascade para rostros
clasificador = cv2.CascadeClassifier(
    cv2.data.haarcascades + 'haarcascade_frontalface_default.xml')

# detectar rostros en la imagen en escala de grises
caras = clasificador.detectMultiScale(gris, scaleFactor=1.1, minNeighbors=5)
print(f'Rostros detectados: {len(caras)}')

# etiqueta segun el contenido de tu imagen
etiqueta = 'Persona'  # cambia a 'Perro' o 'Gato' si es el caso

if len(caras) > 0:
    for (x, y, w, h) in caras:
        # calcular centro y radio del circulo
        centro = (x + w // 2, y + h // 2)
        radio = max(w, h) // 2
        # dibujar circulo verde de grosor 3
        cv2.circle(foto, centro, radio, (0, 255, 0), 3)
        # poner texto encima del circulo
        cv2.putText(foto, etiqueta, (x, y - 10),
                    cv2.FONT_HERSHEY_SIMPLEX, 1, (0, 255, 0), 2)
        print(f'Cara en posicion: x={x}, y={y}, ancho={w}, alto={h}')
else:
    # si no detecta cara, dibujar al centro como respaldo
    h, w = foto.shape[:2]
    cv2.circle(foto, (w // 2, h // 2), min(w, h) // 4, (0, 255, 0), 3)
    cv2.putText(foto, etiqueta, (20, 40),
                cv2.FONT_HERSHEY_SIMPLEX, 1, (0, 255, 0), 2)
    print('No se detecto rostro, se dibujo al centro')

cv2.imwrite('../resultados/5_cara_marcada.png', foto)
print('Imagen guardada con circulo y texto')