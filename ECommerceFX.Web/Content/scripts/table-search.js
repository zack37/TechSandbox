//http://bootsnipp.com/snippets/featured/js-table-filter-simple-insensitive

$(document).ready(function () {
  var activeSystemClass = $('.list-group-item.active');

  $('#system-search').keyup(function () {
    var self = this;
    var tableBody = $('.table-list-search tbody');
    var tableRowsClass = $('.table-list-search tbody tr');
    $('.search-sf').remove();
    tableRowsClass.each(function (i, val) {
      var rowText = $(val).text().toLowerCase();
      var search = $(self).val();
      var inputText = $(self).val().toLowerCase();
      var searchQuerySf = $('.search-query-sf');
      if (inputText !== '') {
        searchQuerySf.remove();
        tableBody.prepend('<tr class="search-query-sf"><td colspan="5"<strong>Searching for: "'
          + search
          + '</strong></td></tr>');
      } else {
        searchQuerySf.remove();
      }

      if (rowText.indexOf(inputText) === -1) {
        tableRowsClass.eq(i).hide();
      } else {
        $('.search-sf').remove();
        tableRowsClass.eq(i).show();
      }

    });

    if (tableRowsClass.children(':visible').length === 0) {
      tableBody.append('<tr class="search-sf"><td class="text-muted" colspan="6">No entries found.</td></tr>');
    }

  });
});