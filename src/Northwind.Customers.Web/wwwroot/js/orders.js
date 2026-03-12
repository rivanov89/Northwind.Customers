document.addEventListener("DOMContentLoaded", function () {
    const tableBody = document.getElementById('orders-table-body');
    const paginationUl = document.getElementById('pagination-controls');
    const pageSizeSelect = document.getElementById('order-page-size');
    const customerId = paginationUl.getAttribute('data-customerid');

    let currentPage = 1;

    async function fetchOrders(page) {
        currentPage = page;
        const pageSize = pageSizeSelect.value; 

        tableBody.style.opacity = "0.5";

        try {
            const url = `/Orders?customerId=${customerId}&page=${page}&pageSize=${pageSize}`;
            const response = await fetch(url);

            if (response.ok) {
                const data = await response.json();
                tableBody.innerHTML = data.html;
                renderPagination(data.currentPage, data.totalPages);
            }
        } catch (err) {
            console.error("Data fetch failed", err);
        } finally {
            tableBody.style.opacity = "1";
        }
    }

    pageSizeSelect.addEventListener('change', () => {
        fetchOrders(1); 
    });

    paginationUl.addEventListener('click', (e) => {
        e.preventDefault();
        const pageAttr = e.target.getAttribute('data-page');
        if (pageAttr) {
            fetchOrders(parseInt(pageAttr));
        }
    });

    function renderPagination(current, total) {
        let html = '';
        if (total <= 1) { paginationUl.innerHTML = ''; return; }

        html += `<li class="page-item ${current === 1 ? 'disabled' : ''}">
                    <a class="page-link" href="#" data-page="${current - 1}">Previous</a>
                 </li>`;

        for (let i = 1; i <= total; i++) {
            html += `<li class="page-item ${i === current ? 'active' : ''}">
                        <a class="page-link" href="#" data-page="${i}">${i}</a>
                     </li>`;
        }

        html += `<li class="page-item ${current === total ? 'disabled' : ''}">
                    <a class="page-link" href="#" data-page="${current + 1}">Next</a>
                 </li>`;

        paginationUl.innerHTML = html;
    }

    fetchOrders(1);
});