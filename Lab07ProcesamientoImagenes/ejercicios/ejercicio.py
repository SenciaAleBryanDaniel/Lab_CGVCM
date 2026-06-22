import cv2

# --- CARGAR LAS 3 IMAGENES ---
rutas = ['../imagenes/img1.jpg', 
         '../imagenes/img2.jpg', 
         '../imagenes/img3.jpg']
imagenes = [cv2.imread(r) for r in rutas]

# --- MOSTRAR TAMAÑOS ORIGINALES ---
for i, im in enumerate(imagenes, 1):
    print(f'Imagen {i}: {im.shape[1]}x{im.shape[0]}')

# --- ENCONTRAR LA MAS GRANDE ---
mayor = max(imagenes, key=lambda im: im.shape[0] * im.shape[1])
alto, ancho = mayor.shape[:2]
print(f'\nLa mas grande mide: {ancho}x{alto}')

# --- REDIMENSIONAR LAS TRES ---
redimensionadas = [cv2.resize(im, (ancho, alto)) for im in imagenes]

# --- GUARDAR RESULTADOS ---
for i, im in enumerate(redimensionadas, 1):
    cv2.imwrite(f'../resultados/1_redimensionada_{i}.png', im)
    print(f'Imagen {i} redimensionada: {im.shape[1]}x{im.shape[0]}')

print('\nListo. Revisa la carpeta resultados/')