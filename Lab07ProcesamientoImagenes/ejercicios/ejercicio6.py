import cv2

# cargar una imagen
img = cv2.imread('../imagenes/img2.jpg')
print(f'Imagen original: {img.shape[1]}x{img.shape[0]}, canales: {img.shape[2]}')

# convertir a escala de grises primero
gris = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
print(f'Imagen en grises: {gris.shape[1]}x{gris.shape[0]}, canales: 1')

# aplicar umbral binario con valor de corte 127
valor_umbral, binaria = cv2.threshold(gris, 127, 255, cv2.THRESH_BINARY)
print(f'Umbral aplicado con valor de corte: {valor_umbral}')

# contar pixeles blancos y negros para ver el resultado
blancos = (binaria == 255).sum()
negros = (binaria == 0).sum()
total = binaria.size
print(f'Pixeles blancos: {blancos} ({blancos*100//total}%)')
print(f'Pixeles negros: {negros} ({negros*100//total}%)')

cv2.imwrite('../resultados/6_grises.png', gris)
cv2.imwrite('../resultados/6_umbral_binario.png', binaria)
print('Imagenes guardadas')