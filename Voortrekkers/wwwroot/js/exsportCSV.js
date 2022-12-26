function exportTableToExcel(name) {
    var table = document.querySelector("table");
    var filename = name + ".xlsx"
    var wb = XLSX.utils.table_to_book(table);
    XLSX.writeFile(wb, filename);
}
