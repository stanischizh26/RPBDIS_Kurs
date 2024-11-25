function delete_and_infoProduct(deleteButton) {
    const row = deleteButton.closest('tr');
    const id = row.dataset.id;

    const modal = document.getElementById("productModal");
    const modalContent = modal.querySelector(".modal-content");

    modalContent.innerHTML = `
        <h3>Are you sure you want to delete the product?</h3>
        <p><strong>Product Name:</strong> ${row.cells[0].innerText}</p>
        <p><strong>Characteristics:</strong> ${row.cells[1].innerText}</p>
        <p><strong>Unit:</strong> ${row.cells[2].innerText}</p>
        <p><strong>Photo:</strong> ${row.cells[3].innerText}</p>
        <button onclick="closeModalProduct()">Close</button>
        <button onclick="deleteProduct('${id}')">Delete</button>
    `;

    modal.style.display = "block";
}

async function deleteProduct(id) {
    try {
        const response = await axios.delete(`${apiBaseUrl}/${id}`);
        if (response.status === 204) {
            const row = document.querySelector(`tr[data-id="${id}"]`);
            if (row) row.remove(); // Удаляем строку из таблицы
            alert("Product has been deleted successfully.");
        } else {
            alert("Failed to delete product.");
        }
    } catch (error) {
        console.error("Error deleting product:", error);
        if (error.response) {
            // Ответ от сервера с ошибкой
            alert(`Server error: ${error.response.status} - ${error.response.statusText}`);
        } else {
            // Ошибка запроса (например, нет соединения)
            alert("Network error: Failed to delete product.");
        }
    }

    // Закрываем модальное окно после удаления
    const modal = document.getElementById("productModal");
    modal.style.display = "none";
}

function closeModalProduct() {
    const modal = document.getElementById("productModal");
    modal.style.display = "none";
}
