import cv2

# cargar la imagen combinada del ejercicio anterior
combinada = cv2.imread('../resultados/2_combinada.png')

# negativo: invertir cada valor de color (255 - valor)
negativo = 255 - combinada
cv2.imwrite('../resultados/3_negativo.png', negativo)
print(f'Negativo creado: {negativo.shape[1]}x{negativo.shape[0]}')

# escala de grises: convertir de BGR a un solo canal de intensidad
grises = cv2.cvtColor(combinada, cv2.COLOR_BGR2GRAY)
cv2.imwrite('../resultados/3_grises.png', grises)
print(f'Escala de grises creada: {grises.shape[1]}x{grises.shape[0]}')
print(f'Canales original: {combinada.shape[2]} -> Canales grises: 1')