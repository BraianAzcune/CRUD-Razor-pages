import { test, expect } from '@playwright/test';

test('Ventana creacion producto', async ({ page }) => {
  await page.goto('https://localhost:7286/');

  await test.step('crear producto', async () => {
    await page.getByRole('link', { name: 'Productos' }).click();
    await page.getByRole('link', { name: 'Agregar Producto' }).click();
    await page.getByLabel('Nombre del producto').click();
    await page.getByLabel('Nombre del producto').fill('prueba1');
    await page.getByLabel('Descripción').click();
    await page.getByLabel('Descripción').fill('creado por playwright');
    await page.getByLabel('Cantidad').click();
    await page.getByLabel('Cantidad').fill('10');
    await page.getByLabel('Precio').click();
    await page.getByLabel('Precio').fill('1500');
    await page.getByRole('button', { name: 'Submit' }).click();
    await page.getByText('producto creado con exito').click();
    await page.getByRole('link', { name: 'Volver al listado' }).click();
    await page.getByRole('cell', { name: 'prueba1' }).click();

  });

  await test.step('detectar producto duplicado', async () => {
    await page.getByRole('link', { name: 'Agregar Producto' }).click();
    await page.getByLabel('Nombre del producto').click();
    await page.getByLabel('Nombre del producto').fill('prueba1');
    await page.getByLabel('Descripción').click();
    await page.getByLabel('Descripción').fill('creado por playwright');
    await page.getByLabel('Cantidad').click();
    await page.getByLabel('Cantidad').fill('10');
    await page.getByLabel('Precio').click();
    await page.getByLabel('Precio').fill('1500');
    await page.getByRole('button', { name: 'Submit' }).click();
    await page.getByText(/^Error/).click();
  });

  await test.step('Borrar producto creado', async () => {
    await page.goto('https://localhost:7286/Productos');

    const filasLocator = page.locator("tr");
    const fila = filasLocator.filter({ has: page.getByRole('cell', { name: 'prueba1' }) })
    fila.getByRole('link', { name: 'Editar' }).click()
    // asegurarse que se esta en el formulario bien cargado
    await page.getByLabel('Nombre del producto').click();

    await page.getByRole("button", { name: "Eliminar producto" }).click();
    await page.getByRole('button', { name: 'Borrar', exact: true }).click();

    await page.getByText(/^producto prueba1 .+ fue borrado exitosamente/)
  });


});
