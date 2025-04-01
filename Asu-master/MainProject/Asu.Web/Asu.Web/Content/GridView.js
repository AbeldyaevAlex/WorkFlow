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
        //pageToolbar.GetItemByName("EditSpecification").SetEnabled(enabled);

        pageToolbar.GetItemByName("Edit").SetEnabled(gridView.GetFocusedRowIndex() !== -1);
        pageToolbar.GetItemByName("Delete").SetEnabled(gridView.GetFocusedRowIndex() !== -1);
        //pageToolbar.GetItemByName("EditSpecification").SetEnabled(gridView.GetFocusedRowIndex() !== -1);

    }
    function onPageToolbarItemClick(s, e) {
        switch (e.item.name) {
            case "ToggleFilterPanel":
                toggleFilterPanel();
                break;
            case "New":
                gridView.AddNewRow();
                break;
            case "Edit":
                gridView.StartEditRow(gridView.GetFocusedRowIndex());
                break;
            case "Delete":
                deleteSelectedRecords();
                break;
            case "Export":
                gridView.ExportTo(ASPxClientGridViewExportFormat.Xlsx);
                break;
            case "Specification":
                updateSpecification();
                break;
            case "Collapse":
                collapse();
                break;
            case "Expand":
                expande();
                break;
            case "vibIzd":
                viborIzdelia();
                break;
            case "Edit Record":
                editRecord();
                break;
        }
    }
    function collapse() {
        gridView.CollapseAll()
    };
    function expande() {
        gridView.ExpandAll()
    };
    function deleteSelectedRecords() {
        if (confirm('Confirm Delete?')) {
            gridView.GetSelectedFieldValues("Id", getSelectedFieldValuesCallback);
        }
    }
    function deleteSelectedRecords() {
        gridView.GetSelectedFieldValues("Id", getSelectedFieldValuesCallback);
    }
    function updateSpecification() {
        gridView.GetSelectedFieldValues("Id", getSelectedSpecification);
    }
    function viborIzdelia() {
        gridView.GetSelectedFieldValues("Id", getIzdelie);
    }
    function editRecord() {
        gridView.GetSelectedFieldValues("Id", getEditRecord);
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
    function getIzdelie(values) {
        selectedIds = values.join(',');
        gridView.PerformCallback({ customAction: selectedIds, customArg: 'izdelie' });
    }
    function getEditRecord(values) {
        selectedIds = values.join(',');
        gridView.PerformCallback({ customAction: selectedIds, customArg: 'editRecord' });
    }
    //
    function Grid_AfterCallback() {
        alert("111");
    }
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
    window.Grid_AfterCallback = Grid_AfterCallback;
})();


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

        pageToolbar.GetItemByName("Edit").SetEnabled(gridView.GetFocusedRowIndex() !== -1);
    }
    function onPageToolbarItemClick(s, e) {
        switch (e.item.name) {
            case "ToggleFilterPanel":
                toggleFilterPanel();
                break;
            case "New":
                gridView.AddNewRow();
                break;
            case "Edit":
                gridView.StartEditRow(gridView.GetFocusedRowIndex());
                break;
            case "Delete":
                deleteSelectedRecords();
                break;
            case "Export":
                gridView.ExportTo(ASPxClientGridViewExportFormat.Xlsx);
                break;
        }
    }
    function deleteSelectedRecords() {
        if (confirm('Confirm Delete?')) {
            gridView.GetSelectedFieldValues("Id", getSelectedFieldValuesCallback);
        }
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
        gridView.PerformCallback({ customAction: 'delete' });
    }

    window.onGridViewBeginCallback = onGridViewBeginCallback;
    window.onGridViewInit = onGridViewInit;
    window.onGridViewSelectionChanged = onGridViewSelectionChanged;
    window.onPageToolbarItemClick = onPageToolbarItemClick;
    window.onFilterPanelExpanded = onFilterPanelExpanded;
    window.onFiltersNavBarItemClick = onFiltersNavBarItemClick;
})();