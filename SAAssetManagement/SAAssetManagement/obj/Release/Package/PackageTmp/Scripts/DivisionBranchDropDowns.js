$(document).ready(function () {

            var divisions = @Html.Raw(@ViewBag.all_divisions);
            var branches = @Html.Raw(@ViewBag.all_branches);

            var savedBranch = parseInt(@Html.Raw(@Model.BranchID));
            var savedDivision = parseInt(@Html.Raw(@Model.DivisionID));

            var getBranches = function (id) {

                $("#branchid").find("option").remove();
                $("#branchid").append("<option value=''>Please select a branch</option>");

                $.each(branches, function (key,value) {
                    if (id == value.division) {
                        if (value.id == savedBranch) {
                            $('#branchid').append($("<option></option>")
                            .attr("selected", "selected")
                            .attr("value", value.id)
                            .text(value.value));
                        }
                        else {
                            $('#branchid').append($("<option></option>")
                            .attr("value", value.id)
                            .text(value.value));
                        }
                    }
                });
            }

            $("#divisionid").find("option").remove();
            $("#divisionid").append("<option value=''>Please select a division</option>");

            var id = 0;

            $.each(divisions, function (key, value) {
                if (value.id == savedDivision) {
                    id = value.id;
                    $('#divisionid').append($("<option></option>")
                    .attr("selected", "selected")
                    .attr("value", value.id)
                    .text(value.value));
                }
                else {
                    $('#divisionid').append($("<option></option>")
                    .attr("value", value.id)
                    .text(value.value));
                }
            });

            getBranches(id);

            $("#divisionid").change(function () {
                var selectedValue = $(this).val();
                getBranches(selectedValue);
            });
        });