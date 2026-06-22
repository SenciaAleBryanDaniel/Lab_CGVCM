import cv2

# cargar las 3 imagenes redimensionadas del ejercicio anterior
img1 = cv2.imread('../resultados/1_redimensionada_1.png')
img2 = cv2.imread('../resultados/1_redimensionada_2.png')
img3 = cv2.imread('../resultados/1_redimensionada_3.png')

# separar cada imagen en sus 3 canales (B, G, R)
b1, g1, r1 = cv2.split(img1)  # nos interesa r1 (rojo)
b2, g2, r2 = cv2.split(img2)  # nos interesa g2 (verde)
b3, g3, r3 = cv2.split(img3)  # nos interesa b3 (azul)

# combinar: merge recibe en orden [B, G, R]
combinada = cv2.merge([b3, g2, r1])

cv2.imwrite('../resultados/2_combinada.png', combinada)
print(f'Imagen combinada creada: {combinada.shape[1]}x{combinada.shape[0]}')
print('Canales: Rojo=img1, Verde=img2, Azul=img3')