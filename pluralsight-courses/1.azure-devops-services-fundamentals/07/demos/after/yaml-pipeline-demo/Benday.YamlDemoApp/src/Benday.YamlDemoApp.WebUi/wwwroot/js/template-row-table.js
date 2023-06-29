function addTableRow(tableId, rowCountId, templateRowId) {
    var rowCountElementId = '#' + rowCountId;
    var hiddenRowCountControl = $(rowCountElementId);
    var nextRowIndex = hiddenRowCountControl.val();

    var templateRow = $('#' + templateRowId).html();

    templateRow = templateRow.replace(/ROWID/g, nextRowIndex);

    var tableBodyId = '#' + tableId + ' tbody';

    var table = $(tableBodyId);

    table.append(templateRow);

    nextRowIndex++;

    hiddenRowCountControl.val(nextRowIndex);
}

function markForDeleteOrUndelete(deleteUndeleteButtonId, rowId, isMarkedForDeleteId) {
    var theButton = document.getElementById(deleteUndeleteButtonId);
    var theRow = document.getElementById(rowId);
    var theHiddenValueForIsMarkedForDelete = document.getElementById(isMarkedForDeleteId);

    var childElements = theRow.getElementsByTagName('input');

    var setDisabledToValue; 

    if (theHiddenValueForIsMarkedForDelete.value === "true" ||
        theHiddenValueForIsMarkedForDelete.value === "True") {
        theButton.textContent = "delete";
        setDisabledToValue = false;
        theHiddenValueForIsMarkedForDelete.value = "false";
    } else {
        theButton.textContent = "undelete";
        setDisabledToValue = true;
        theHiddenValueForIsMarkedForDelete.value = "true";
    }    

    Array.prototype.forEach.call(childElements, function (element) {
        if (setDisabledToValue === true) {
            element.classList.add('disabled-field');
        } else {
            element.classList.remove('disabled-field');
        }
    });
}