let sort = null
let yearFrom = null
let yearTo = null
let nameValue = null
let descriptionValue = null

$(function () {
    $('#nameInput').on('blur', function () {
        nameValue = $(this).val();
        currentPage = 1;
        loadData();
    })

    $('#descriptionInput').on('blur', function () {
        descriptionValue = $(this).val();
        currentPage = 1;
        loadData();
    })

    $('#releaseYearFromInput').on('blur', function () {
        yearFrom = $(this).val();
        currentPage = 1;
        loadData();
    })

    $('#releaseYearToInput').on('blur', function () {
        yearTo = $(this).val();
        currentPage = 1;
        loadData();
    })

    $('#sortSelect').on('change', function () {
        sort = $(this).val();
        currentPage = 1;
        loadData();
    })

    registerBtns();
});

const loadData = function () {
    $('.spinner').removeClass("spinnerHidden");
    $("#table").html("");

    $.get('/Albums/GetList',
        {
            pageSize: pageSize,
            page: currentPage,
            sortOrder: sort,
            name: nameValue,
            description: descriptionValue,
            releaseYearFrom: yearFrom,
            releaseYearTo: yearTo
        })
        .done(function (data) {
            $('.spinner').addClass("spinnerHidden");
            $("#table").html(data);
            registerBtns();
        })
}

const registerBtns = function () {

    $('.changePageSize').on('click', function (e) {
        e.preventDefault();
        pageSize = e.target.value;
        currentPage = 1;
        loadData();
    })

    $('.prevPageBtn').on('click', function (e) {
        e.preventDefault();
        currentPage--;
        loadData();
    })

    $('#changePageBtn1').on('click', function (e) {
        e.preventDefault();
        currentPage = $("#pageValue1").val();
        if (currentPage > totalPages)
            currentPage = totalPages;
        loadData();
    })

    $('#changePageBtn2').on('click', function (e) {
        e.preventDefault();
        currentPage = $("#pageValue2").val();
        if (currentPage > totalPages)
            currentPage = totalPages;
        loadData();
    })

    $('.nextPageBtn').on('click', function (e) {
        e.preventDefault();
        currentPage++;
        loadData();
    })

}
