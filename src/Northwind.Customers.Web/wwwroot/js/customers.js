document.addEventListener("DOMContentLoaded", function () {
    const tableBody = document.getElementById('customers-table-body');
    const paginationUl = document.getElementById('customer-pagination');
    const searchInput = document.getElementById('customer-search');
    const pageSizeSelect = document.getElementById('page-size-select');

    let state = {
        page: 1,
        pageSize: 10,
        searchTerm: ''
    };

    async function loadCustomers() {
        tableBody.style.opacity = "0.5";
        const url = `/GetList?page=${state.page}&pageSize=${state.pageSize}&searchTerm=${encodeURIComponent(state.searchTerm)}`;

        try {
            const response = await fetch(url);
            const data = await response.json();

            tableBody.innerHTML = data.html;
            renderPagination(data.currentPage, data.totalPages);
        } catch (err) {
            console.error("Error:", err);
        } finally {
            tableBody.style.opacity = "1";
        }
    }

    function renderPagination(current, total) {
        let html = '';
        if (total <= 1) { paginationUl.innerHTML = ''; return; }

        html += `<li class="page-item ${current === 1 ? 'disabled' : ''}"><a class="page-link" href="#" data-page="${current - 1}">Prev</a></li>`;

        for (let i = 1; i <= total; i++) {
            html += `<li class="page-item ${i === current ? 'active' : ''}"><a class="page-link" href="#" data-page="${i}">${i}</a></li>`;
        }

        html += `<li class="page-item ${current === total ? 'disabled' : ''}"><a class="page-link" href="#" data-page="${current + 1}">Next</a></li>`;
        paginationUl.innerHTML = html;
    }

    // Listeners
    paginationUl.addEventListener('click', e => {
        e.preventDefault();
        const p = e.target.getAttribute('data-page');
        if (p) { state.page = parseInt(p); loadCustomers(); }
    });

    pageSizeSelect.addEventListener('change', e => {
        state.pageSize = e.target.value;
        state.page = 1;
        loadCustomers();
    });

    let debounceTimer;
    searchInput.addEventListener('input', e => {
        clearTimeout(debounceTimer);
        debounceTimer = setTimeout(() => {
            state.searchTerm = e.target.value;
            state.page = 1;
            loadCustomers();
        }, 300);
    });

    loadCustomers(); // Initial call
});