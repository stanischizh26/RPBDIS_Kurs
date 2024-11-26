function delete_and_infoEnterprise(deleteButton) {
    const row = deleteButton.closest('tr');
    const id = row.dataset.id;

    const model = document.getElementById("enterpriseModel");
    const modelContent = model.querySelector(".model-content");

    modelContent.innerHTML = `
        <h3>Вы уверены, что хотите удалить предприятие?</h3>
        <p><strong>Enterprise Name:</strong> ${row.cells[0].innerText}</p>
        <p><strong>DirectorName:</strong> ${row.cells[1].innerText}</p>
        <p><strong>ActivityType:</strong> ${row.cells[2].innerText}</p>
        <p><strong>OwnershipForm:</strong> ${row.cells[3].innerText}</p>
        <button onclick="closeModelEnterprise()">Close</button>
        <button onclick="deleteEnterprise('${id}')">Delete</button>
    `;

    model.style.display = "block";
}

async function deleteEnterprise(id) {
    try {
        const response = await axios.delete(`${apiBaseUrl}/${id}`);
        if (response.status === 204) {
            const row = document.querySelector(`tr[data-id="${id}"]`);
            if (row) row.remove(); // Удаляем строку из таблицы
            alert("The enterprise was successfully deleted.");
        } else {
            alert("The enterprise could not be deleted.");
        }
    } catch (error) {
        console.error("Error when deleting an enterprise:", error);
        if (error.response) {
            // Ответ от сервера с ошибкой
            alert(`Server error: ${error.response.status} - ${error.response.statusText}`);
        } else {
            // Ошибка запроса (например, нет соединения)
            alert("network error: The company could not be deleted.");
        }
    }

    // Закрываем модальное окно после удаления
    const model = document.getElementById("enterpriseModel");
    model.style.display = "none";
}

function closeModelEnterprise() {
    const model = document.getElementById("enterpriseModel");
    model.style.display = "none";
}
