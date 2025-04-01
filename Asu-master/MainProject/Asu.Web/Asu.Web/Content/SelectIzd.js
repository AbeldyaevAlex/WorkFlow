"use strict";
<script type="text/javascript">
    $(document).ready(function () {
        $("$TemId").change(function () {
            var temId = $(this).val();
            $.ajax({
                type: "Post",
                url: "/Test3/GetPerIzd?link_tema=" + temId,
                contentType: "html",
                success: function (response) {
                    debugger
                    $("PerIzdId").empty();
                    $("PerIzdId").append(response);
                }
            })
        })
    })
</script>
