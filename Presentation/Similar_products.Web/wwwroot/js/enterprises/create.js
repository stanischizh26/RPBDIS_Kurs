function addEmptyRowEnterprise() {
    const table = document.querySelector("#enterprises-container table tbody");

    // Создаём новую строку
    const newRow = document.createElement("tr");
    newRow.dataset.id = "new"; // Временный ID для новой строки

    newRow.innerHTML = `
        <td style="padding: 8px;" contenteditable="true"></td> <!-- Name -->
        <td style="padding: 8px;" contenteditable="true"></td> <!-- DirectorName -->
        <td style="padding: 8px;" contenteditable="true"></td> <!-- ActivityType -->
        <td style="padding: 8px;" contenteditable="true"></td> <!-- OwnershipForm -->
        <td style="padding: 8px;">
            <a href="javascript:void(0);" onclick="saveNewRowEnterprise(this)" title="Save">
                <i class="bi bi-check-circle-fill"></i>
            </a>
            <a href="javascript:void(0);" onclick="cancelNewRowEnterprise(this)" title="Cancel">
                <i class="bi bi-x-circle-fill"></i>
            </a>
        </td>
    `;

    // Вставляем новую строку в начало таблицы
    table.prepend(newRow);
}

async function saveNewRowEnterprise(saveButton) {
    const row = saveButton.closest("tr");
    const cells = row.querySelectorAll("td[contenteditable]");

    // Собираем данные из строки
    const newEnterprise = {
        name: cells[0].innerText.trim(),
        directorName: cells[1].innerText.trim(),
        activityType: cells[2].innerText.trim(),
        ownershipForm: cells[3].innerText.trim()
    };

    // Проверяем заполненность полей
    if (!newEnterprise.name || !newEnterprise.directorName || !newEnterprise.activityType || !newEnterprise.ownershipForm) {
        alert("All fields (Name, DirectorName, ActivityType, OwnershipForm ) must be filled.");
        return;
    }

    try {
        // Отправляем данные на сервер
        const response = await axios.post(apiBaseUrl, newEnterprise);

        if (response.status === 201) {
            alert("Enterprise created successfully!");

            // Обновляем строку с новыми данными
            row.dataset.id = response.data.id; // Устанавливаем ID, полученный от сервера
            row.innerHTML = `
                <td style="padding: 8px;" contenteditable="false">${response.data.name}</td>
                <td style="padding: 8px;" contenteditable="false">${response.data.directorName}</td>
                <td style="padding: 8px;" contenteditable="false">${response.data.activityType}</td>
                <td style="padding: 8px;" contenteditable="false">${response.data.ownershipForm}</td>
                <td style="padding: 8px;">
                    <a href="javascript:void(0);" onclick="editRowEnterprise(this)" title="Edit">
                        <i class="bi bi-pencil-fill"></i>
                    </a>
                    <a href="javascript:void(0);" onclick="delete_and_infoEnterprise(this)" title="Delete Item">
                        <i class="bi bi-trash-fill"></i>
                    </a>
                </td>
            `;
        } else {
            throw new Error("Failed to create Enterprise.");
        }
    } catch (error) {
        console.error("Error creating Enterprise:", error);
        alert("Failed to create Enterprise. Please try again.");

        // Удаляем строку при ошибке
        row.remove();
    }
}

function cancelNewRowEnterprise(cancelButton) {
    const row = cancelButton.closest("tr");
    row.remove(); // Удаляем строку
}
