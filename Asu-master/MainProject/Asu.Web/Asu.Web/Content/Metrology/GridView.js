(function () {
    var selectedIds;
    function onGridViewInit(s, e) {
        AddAdjustmentDelegate(adjustGridView);
        updateToolbarButtonsState();
    }
    function onGridViewSelectionChanged(s, e) {
        updateToolbarButtonsState();
    }
    function adjustGridView() {
        gridView.AdjustControl();
    }
    function updateToolbarButtonsState() {
        var enabled = gridView.GetSelectedRowCount() > 0;
        pageToolbar.GetItemByName("Delete").SetEnabled(enabled);
        pageToolbar.GetItemByName("Export").SetEnabled(enabled);
        pageToolbar.GetItemByName("New").SetEnabled(enabled);
        //pageToolbar.GetItemByName("EditSpecification").SetEnabled(enabled);

        pageToolbar.GetItemByName("Edit").SetEnabled(gridView.GetFocusedRowIndex() !== -1);
        pageToolbar.GetItemByName("Delete").SetEnabled(gridView.GetFocusedRowIndex() !== -1);
        //pageToolbar.GetItemByName("EditSpecification").SetEnabled(gridView.GetFocusedRowIndex() !== -1);

    }
    function onPageToolbarItemClick(s, e) {
        //alert(e.item.name);
        switch (e.item.name) {
            case "ToggleFilterPanel":
                toggleFilterPanel();
                break;
            case "New":
                //alert("New");
                //gridView.AddNewRow();
                pcModalInsert.Show();
                //pcModalExcel.Show();              
                break;
            case "Edit":
                //alert("Edit");
                gridView.StartEditRow(gridView.GetFocusedRowIndex());
                //pcModalEdit.Show(gridView.GetFocusedRowIndex());
                //pcModalEdit.Show(selectedIds);
                break;
            case "Delete":
                //alert("Delete");
                deleteSelectedRecords();
                //alert(selectedIds);
                //alert(gridView.GetFocusedRowIndex());
                //alert(gridView.GetFocusedRowIndex());
                //pcModalDelete.Show(gridView.GetFocusedRowIndex());
                break;
            case "Export":
                //gridView.ExportTo(ASPxClientGridViewExportFormat.Xlsx);
                pcModalExcel.Show();
                break;
            case "ExportIstor":
                //gridView.ExportTo(ASPxClientGridViewExportFormat.Xlsx);
                pcModalExcelIstor.Show();
                break;

            case "Specification":
                updateSpecification();
                break;
        }
    }
    function onPopupNameDeleteClick() {
        //alert(selectedIds);
        //alert(gridView.GetFocusedRowIndex());
        //deleteSelectedRecords();
        gridView.PerformCallback({ customAction: selectedIds, customArg: 'delete' });
    }


    function deleteSelectedRecords() {
        if (confirm("Удалить текущую запись?")) {
            gridView.GetSelectedFieldValues("Id", getSelectedFieldValuesCallback);
        }
    }
    //function deleteSelectedRecords() {
    //    gridView.GetSelectedFieldValues("Id", getSelectedFieldValuesCallback);
    //    //pcModalDelete
    //}
    function updateSpecification() {
        gridView.GetSelectedFieldValues("Id", getSelectedSpecification);
    }



    function onFiltersNavBarItemClick(s, e) {
        var filters = {
            All: "",
            Active: "[Status] = 1",
            Bugs: "[Kind] = 1",
            Suggestions: "[Kind] = 2",
            HighPriority: "[Priority] = 1"
        };
        gridView.ApplyFilter(filters[e.item.name]);
        HideLeftPanelIfRequired();
    }

    function toggleFilterPanel() {
        filterPanel.Toggle();
    }

    function onFilterPanelExpanded(s, e) {
        adjustPageControls();
        searchButtonEdit.SetFocus();
    }

    function onGridViewBeginCallback(s, e) {
        e.customArgs['SelectedRows'] = selectedIds;
    }


    function getSelectedFieldValuesCallback(values) {
        selectedIds = values.join(',');
        gridView.PerformCallback({ customAction: selectedIds, customArg: 'delete' });
    }
    function getSelectedSpecification(values) {
        selectedIds = values.join(',');
        gridView.PerformCallback({ customAction: selectedIds, customArg: 'specification' });
    }
    //

    //function getSelectedFieldValue(values) {
    //    selectedIds = values.join(',');
    //    gridView.PerformCallback({ customAction: 'addspecif' });
    //}

    //function getSelectedFieldValuesCallback(values) {
    //    selectedIds = values.join(',');
    //    gridView.PerformCallback({ customAction: 'delete' });
    //}


    window.onGridViewBeginCallback = onGridViewBeginCallback;
    window.onGridViewInit = onGridViewInit;
    window.onGridViewSelectionChanged = onGridViewSelectionChanged;
    window.onPageToolbarItemClick = onPageToolbarItemClick;
    window.onFilterPanelExpanded = onFilterPanelExpanded;
    window.onFiltersNavBarItemClick = onFiltersNavBarItemClick;
})();

//(function () {
//    var selectedIds;
//    function onGridViewInit(s, e) {
//        AddAdjustmentDelegate(adjustGridView);
//        updateToolbarButtonsState();
//    }
//    function onGridViewSelectionChanged(s, e) {
//        updateToolbarButtonsState();
//    }
//    function adjustGridView() {
//        gridView.AdjustControl();
//    }
//    function updateToolbarButtonsState() {
//        var enabled = gridView.GetSelectedRowCount() > 0;
//        pageToolbar.GetItemByName("Delete").SetEnabled(enabled);
//        pageToolbar.GetItemByName("Export").SetEnabled(enabled);

//        pageToolbar.GetItemByName("Edit").SetEnabled(gridView.GetFocusedRowIndex() !== -1);
//    }
//    function onPageToolbarItemClick(s, e) {
//        switch (e.item.name) {
//            case "ToggleFilterPanel":
//                toggleFilterPanel();
//                break;
//            case "New":
//                gridView.AddNewRow();
//                break;
//            case "Edit":
//                gridView.StartEditRow(gridView.GetFocusedRowIndex());
//                break;
//            case "Delete":
//                deleteSelectedRecords();
//                break;
//            case "Export":
//                gridView.ExportTo(ASPxClientGridViewExportFormat.Xlsx);
//                break;
//        }
//    }
//    function deleteSelectedRecords() {
//        if (confirm('Confirm Delete?')) {
//            gridView.GetSelectedFieldValues("Id", getSelectedFieldValuesCallback);
//        }
//    }
//    function onFiltersNavBarItemClick(s, e) {
//        var filters = {
//            All: "",
//            Active: "[Status] = 1",
//            Bugs: "[Kind] = 1",
//            Suggestions: "[Kind] = 2",
//            HighPriority: "[Priority] = 1"
//        };
//        gridView.ApplyFilter(filters[e.item.name]);
//        HideLeftPanelIfRequired();
//    }

//    function toggleFilterPanel() {
//        filterPanel.Toggle();
//    }

//    function onFilterPanelExpanded(s, e) {
//        adjustPageControls();
//        searchButtonEdit.SetFocus();
//    }

//    function onGridViewBeginCallback(s, e) {
//        e.customArgs['SelectedRows'] = selectedIds;
//    }
//    function getSelectedFieldValuesCallback(values) {
//        selectedIds = values.join(',');
//        gridView.PerformCallback({ customAction: 'delete' });
//    }

//    window.onGridViewBeginCallback = onGridViewBeginCallback;
//    window.onGridViewInit = onGridViewInit;
//    window.onGridViewSelectionChanged = onGridViewSelectionChanged;
//    window.onPageToolbarItemClick = onPageToolbarItemClick;
//    window.onFilterPanelExpanded = onFilterPanelExpanded;
//    window.onFiltersNavBarItemClick = onFiltersNavBarItemClick;
//})();