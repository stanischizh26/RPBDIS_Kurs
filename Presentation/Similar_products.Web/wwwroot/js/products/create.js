function addEmptyRowProduct() {
    const table = document.querySelector("#products-container table tbody");

    // Создаём новую строку
    const newRow = document.createElement("tr");
    newRow.dataset.id = "new"; // Временный ID для новой строки

    newRow.innerHTML = `
        <td style="padding: 8px;" contenteditable="true"></td> <!-- Name -->
        <td style="padding: 8px;" contenteditable="true"></td> <!-- Characteristics -->
        <td style="padding: 8px;" contenteditable="true"></td> <!-- Unit -->
        <td style="padding: 8px;" contenteditable="true"></td> <!-- Photo -->
        <td style="padding: 8px;">
            <a href="javascript:void(0);" onclick="saveNewRowProduct(this)" title="Save">
                <i class="bi bi-check-circle-fill"></i>
            </a>
            <a href="javascript:void(0);" onclick="cancelNewRowProduct(this)" title="Cancel">
                <i class="bi bi-x-circle-fill"></i>
            </a>
        </td>
    `;

    // Вставляем новую строку в начало таблицы
    table.prepend(newRow);
}

async function saveNewRowProduct(saveButton) {
    const row = saveButton.closest("tr");
    const cells = row.querySelectorAll("td[contenteditable]");

    // Собираем данные из строки
    const newProduct = {
        name: cells[0].innerText.trim(),
        characteristics: cells[1].innerText.trim(),
        unit: cells[2].innerText.trim(),
        photo: cells[3].innerText.trim()
    };

    // Проверяем заполненность полей
    if (!newProduct.name || !newProduct.characteristics || !newProduct.unit) {
        alert("All fields (Name, Characteristics, Unit) must be filled.");
        return;
    }

    try {
        // Отправляем данные на сервер
        const response = await axios.post(apiBaseUrl, newProduct);

        if (response.status === 201) {
            alert("Product created successfully!");

            // Обновляем строку с новыми данными
            row.dataset.id = response.data.id; // Устанавливаем ID, полученный от сервера
            row.innerHTML = `
                <td style="padding: 8px;" contenteditable="false">${response.data.name}</td>
                <td style="padding: 8px;" contenteditable="false">${response.data.characteristics}</td>
                <td style="padding: 8px;" contenteditable="false">${response.data.unit}</td>
                <td style="padding: 8px;" contenteditable="false">${response.data.photo}</td>
                <td style="padding: 8px;">
                    <a href="javascript:void(0);" onclick="editRowProduct(this)" title="Edit">
                        <i class="bi bi-pencil-fill"></i>
                    </a>
                    <a href="javascript:void(0);" onclick="delete_and_infoProduct(this)" title="Delete Item">
                        <i class="bi bi-trash-fill"></i>
                    </a>
                </td>
            `;
        } else {
            throw new Error("Failed to create Product.");
        }
    } catch (error) {
        console.error("Error creating Product:", error);
        alert("Failed to create Product. Please try again.");

        // Удаляем строку при ошибке
        row.remove();
    }
}

function cancelNewRowProduct(cancelButton) {
    const row = cancelButton.closest("tr");
    row.remove(); // Удаляем строку
}
