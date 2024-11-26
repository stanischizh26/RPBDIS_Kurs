const apiBaseUrl = "/api/enterprises"; 
let currentPage = 1;
const itemsPerPage = 10;

function renderEnterprises(items, totalItems, currentPage) {
    const container = document.getElementById("enterprises-container");
    container.innerHTML = "";

    if (items.length === 0) {
        container.innerHTML = `No enterprises found.`;
        return;
    }

    const table = document.createElement("table");
    const caption = document.createElement("caption");
    caption.innerHTML = `
        Enterprise List
        <a href="javascript:void(0);" onclick="addEmptyRowEnterprise()" title="Add Item">
            <i class="bi bi-plus-square-fill"></i>
        </a>`;
    table.appendChild(caption);

    const thead = document.createElement("thead");
    thead.innerHTML = `
        <tr>
            <th>Name</th>
            <th>DirectorName</th>
            <th>ActivityType</th>
            <th>OwnershipForm</th>
            <th>Actions</th>
        </tr>`;
    table.appendChild(thead);

    const tbody = document.createElement("tbody");
    tbody.innerHTML = items
        .map(
            (item) => `
        <tr data-id="${item.id}">
            <td contenteditable="false">${item.name}</td>
            <td contenteditable="false">${item.directorName}</td>
            <td contenteditable="false">${item.activityType}</td>
            <td contenteditable="false">${item.ownershipForm}</td>
            <td class="actions">
                <a href="javascript:void(0);" onclick="editRowEnterprise(this)" title="Edit">
                    <i class="bi bi-pencil-fill"></i>
                </a>
                <a href="javascript:void(0);" onclick="delete_and_infoEnterprise(this)" title="Delete Item">
                    <i class="bi bi-info-square-fill"></i>
                </a>
            </td>
        </tr>`
        )
        .join("");


    
    table.appendChild(tbody);
    container.appendChild(table);

    // Рендерим пагинацию
    renderPagination(totalItems, currentPage);
}

function renderPagination(totalItems, currentPage) {
    const container = document.getElementById("enterprises-container");
    const totalPages = Math.ceil(totalItems / itemsPerPage);

    const paginationDiv = document.createElement("div");
    paginationDiv.style.textAlign = "center";
    paginationDiv.style.marginTop = "10px";

    // Кнопка для перехода на предыдущую страницу
    const prevButton = document.createElement("button");
    prevButton.innerText = "←";
    prevButton.style.margin = "0 5px";
    prevButton.style.padding = "5px 10px";
    prevButton.style.background = currentPage > 1 ? "#3498DB" : "#ccc";
    prevButton.style.color = currentPage > 1 ? "#fff" : "#666";
    prevButton.style.border = "1px solid #ccc";
    prevButton.style.borderRadius = "4px";
    prevButton.style.cursor = currentPage > 1 ? "pointer" : "not-allowed";
    prevButton.onclick = () => {
        if (currentPage > 1) loadEnterprises(currentPage - 1);
    };

    paginationDiv.appendChild(prevButton);

    // Текущая страница и поле для ввода номера страницы
    const currentPageText = document.createElement("span");
    currentPageText.innerText = `Page ${currentPage} of ${totalPages}`;
    currentPageText.style.margin = "0 10px";
    paginationDiv.appendChild(currentPageText);

    const pageInput = document.createElement("input");
    pageInput.type = "number";
    pageInput.min = 1;
    pageInput.max = totalPages;
    pageInput.value = currentPage;
    pageInput.style.width = "50px";
    pageInput.style.margin = "0 5px";
    pageInput.onchange = () => {
        const inputPage = parseInt(pageInput.value, 10);
        if (!isNaN(inputPage)) {
            if (inputPage < 1) loadEnterprises(1);
            else if (inputPage > totalPages) loadEnterprises(totalPages);
            else loadEnterprises(inputPage);
        }
    };
    paginationDiv.appendChild(pageInput);

    // Кнопка для перехода на следующую страницу
    const nextButton = document.createElement("button");
    nextButton.innerText = "→";
    nextButton.style.margin = "0 5px";
    nextButton.style.padding = "5px 10px";
    nextButton.style.background = currentPage < totalPages ? "#3498DB" : "#ccc";
    nextButton.style.color = currentPage < totalPages ? "#fff" : "#666";
    nextButton.style.border = "1px solid #ccc";
    nextButton.style.borderRadius = "4px";
    nextButton.style.cursor = currentPage < totalPages ? "pointer" : "not-allowed";
    nextButton.onclick = () => {
        if (currentPage < totalPages) loadEnterprises(currentPage + 1);
    };

    paginationDiv.appendChild(nextButton);
    container.appendChild(paginationDiv);
}

async function loadEnterprises(page = 1) {
    try {
        const nameFilter = document.getElementById("filter-name")?.value || "";

        const response = await axios.get(`${apiBaseUrl}`, {
            params: {
                page,
                pageSize: itemsPerPage,
                name: nameFilter,
            },
        });

        const pageResult = response.data;

        // Рендерим продукты и пагинацию
        renderEnterprises(pageResult.items, pageResult.totalCount, page);
    } catch (error) {
        console.error("Error fetching enterprises:", error);
        document.getElementById("enterprises-container").innerHTML =
            `<p>Error loading enterprises. Please try again later.</p>`;
    }
}

function applyFilters() {
    currentPage = 1; // Сбрасываем на первую страницу
    loadEnterprises(currentPage);
}

// Инициализация
loadEnterprises();
