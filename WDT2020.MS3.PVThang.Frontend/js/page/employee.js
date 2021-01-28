$(document).ready(function () {
    var customer = new Customer();
})

class Customer {
    constructor() {
        this.formModes = {
            NONE: null,
            ADD: "add",
            EDIT: "edit"
        }
        this.formMode;
        this.currentPage = 1;
        this.pages = 1;

        this.loadPosition();
        this.loadDepartment();
        this.loadData();
        this.initEvent();
    }

    initEvent() {
        $(".icon--refresh").click(this.loadData.bind(this));
        $(".add--employee").click(this.addOnClick.bind(this));
        $(".dialog__add__header .icon, .button--cancel").click(this.cancelOnClick.bind(this));
        $(".content .table tbody").on("dblclick", "tr", this.trOndblClick.bind(this));
        $(".content .table tbody").on("click", "tr", this.trOnClick.bind(this));
        $(".footer__center .prev--page").click(() => this.changePageOnClick(this.currentPage - 1));
        $(".footer__center .next--page").click(() => this.changePageOnClick(this.currentPage + 1));
        $(".footer__center .first--page").click(() => this.changePageOnClick(1));
        $(".footer__center .last--page").click(() => this.changePageOnClick(this.pages));
        $("#list--page").on("click", ".number--page", () => this.changePageOnClick(new Number($(event.target).html())));
        $("#dial").dialog({
            autoOpen: false,
            resizable: false,
            width: "auto",
            height: "auto",
            modal: true,
            close: function () {
                $('.row--select').map((index, element) => { $(element).removeClass("row--select") });
                $('.title__button .delete--employee').attr('disabled', true);
            }
        });
        $("#dialog__delete").dialog({
            classes: "dialog__delete",
            autoOpen: false,
            resizable: false,
            width: "auto",
            height: "auto",
            modal: true,
        });
        $(".required input").blur((event) => this.checkRequired(event));
        $("#btnSave").click(() => this.saveOnClick());
        $('.title__button .delete--employee').click(this.deleteOnClick.bind(this));
        $('#dialog__delete .yes').click(this.deleteEmployee.bind(this));
        $('#dialog__delete .no').click(() => { $("#dialog__delete").dialog('close') });
        $('.option__filter .filter__department select').on('change', () => { this.currentPage = 1; this.loadData() });
        $('.option__filter .filter__position select').on('change', () => { this.currentPage = 1; this.loadData() });
    }

    loadDepartment() {
        $.ajax({
            url: 'http://localhost:49779/api/departments',
            method: 'GET',
            data: null,
            dataType: 'json',
            contentType: 'application/json'
        }).done(function (res) {
            $.each(res, function (index, item) {
                var optionHTML = `<option value="` + item.DepartmentId + `">` + item.DepartmentName + `</option>`;
                $(".filter__department select, .department select").append(optionHTML);
            })
        }).fail(function () {
            alert('load department: fail!');
        })
    }

    loadPosition() {
        $.ajax({
            url: 'http://localhost:49779/api/Positions',
            method: 'GET',
            data: null,
            dataType: 'json',
            contentType: 'application/json'
        }).done(function (res) {
            $.each(res, function (index, item) {
                var optionHTML = `<option value="` + item.PositionId + `">` + item.PositionName + `</option>`;
                $(".filter__position select, .position select").append(optionHTML);
            })
        }).fail(function () {
            alert('load position: fail!');
        })
    }

    loadData() {
        var self = this;
        var departmentId = $('.option__filter .filter__department select').val();
        var positionId = $('.option__filter .filter__position select').val();
        $.ajax({
            url: 'http://localhost:49779/api/employees/count/' + departmentId + '&' + positionId,
            method: 'GET',
            data: null,
            dataType: 'json',
            contentType: 'application/json'
        }).done(function (res) {
            self.pages = Math.ceil(res/15);
            $("#total--employees").html(res);
            var listPage = ``;
            $("#list--page").empty();
            for (var i = -2; i <= 2; ++i) {
                if (self.currentPage + i >= 1 && self.currentPage + i <= self.pages) {
                    if (i === 0) {
                        listPage += `<div class="btn--second number--page current--page">` + (self.currentPage + i) + `</div>`
                    } else {
                        listPage += `<div class="btn--second number--page">` + (self.currentPage + i) + `</div>`
                    }
                }
            }
            $("#list--page").append(listPage);
            $(".footer__center button.prev--page").attr("disabled", self.currentPage == 1 ? true : false);
            $(".footer__center button.next--page").attr("disabled", self.currentPage == self.pages ? true : false);
            $(".footer__center button.first--page").attr("disabled", self.currentPage == 1 ? true : false);
            $(".footer__center button.last--page").attr("disabled", self.currentPage == self.pages ? true : false);
        }).fail(function () {
            alert('loadData 1: fail!');
        })
        //
        $.ajax({
            url: 'http://localhost:49779/api/employees/' + self.currentPage + '&15' + "&" + departmentId + "&" + positionId,
            method: 'GET',
            data: null,
            dataType: 'json',
            contentType: 'application/json'
        }).done(function (res) {
            $(".content table tbody").empty();
            $.each(res, function (index, item) {
                var trHTML = `  <tr ` + (index % 2 == 0 ? `class="row--odd"` : ``) + ` id="` + item.EmployeeId +`">
                                <td>`+ item.EmployeeCode + `</td>
                                <td>`+ item.FullName + `</td>
                                <td>`+ item.GenderName + `</td>
                                <td>`+ (item.DateOfBirth!==null? self.formatDate(item.DateOfBirth):``) + `</td>
                                <td>`+ item.PhoneNumber + `</td>
                                <td>`+ item.Email + `</td>
                                <td>`+ item.PositionName + `</td>
                                <td>`+ item.DepartmentName + `</td>
                                <td>`+ self.formatMoney(item.Salary) + `</td>
                                <td>`+ item.JobStatusName + `</td>
                            </tr>`
                $(".content table tbody").append(trHTML);
            })
            //
            var employeeStart = 15 * (self.currentPage - 1);
            var employeeEnd = employeeStart + res.length;
            if (res.length > 0) ++employeeStart;
            $("#display--employees").html(employeeStart + "-" + employeeEnd);
            //
            $(".content .footer .footer__right").html("15 nhân viên/trang");
            //
        }).fail(function () {
            alert('loadData employees: fail!');
        })
    }

    /**
     * @param {Date} date
     */
    formatDate(date) {
        var date = new Date(date);
        var day = date.getDate();
        var month = date.getMonth() + 1;
        var year = date.getYear() + 1900;
        if (day < 10) {
            day = '0' + day;
        } if (month < 10) {
            month = '0' + month;
        }
        return day + '/' + month + '/' + year;
    }

    /**
     * return: YYYY-MM-DD
     * @param {any} date
     */
    FormatDate_Y_M_D(date) {
        var d = new Date(date);
        var date = new Date(date);
        var day = date.getDate();
        var month = date.getMonth() + 1;
        var year = date.getYear() + 1900;
        if (day < 10) {
            day = '0' + day;
        } if (month < 10) {
            month = '0' + month;
        }
        return year + '-' + month + '-' + day;
    }

    /**
     * @param {Int32Array} money
     */
    formatMoney(money) {
        var ret = "";
        var count = 0;
        while (money > 0) {
            ++count;
            ret = (money % 10) + "" + ret;
            money = Math.floor(money / 10);
            if (count === 3) {
                ret = '.' + ret;
                count = 0;
            }
        }
        if (ret.length == 0) ret = "0";
        return ret;
    }

    checkFormat(filter, target) {
        if (!filter.test($(target).val())) {
            $(target).parent().addClass("required--error");
            $(target).parent().attr("title", "Hãy nhập đúng định dạng!");
        } else {
            $(target).parent().removeClass("required--error");
            $(target).parent().removeAttr("title");
        }
    }

    deleteOnClick() {
        $("#dialog__delete").dialog("open");
        $('#dialog__delete .employee--code--delete').html($(".row--select td:first").html());
        
    }

    deleteEmployee() {
        var self = this;
        if ($('.row--select').length > 0) {
            var EmployeeId = $('.row--select').attr('id');
            $.ajax({
                url: 'http://localhost:49779/api/Employees/' + EmployeeId,
                method: 'DELETE',
                data: null,
                dataType: 'json',
                contentType: 'application/json'
            }).done(function (res) {
                self.loadData();
                $("#dialog__delete").dialog('close')
            }).fail(function () {
                alert('delete employee: fail!');
            });
        }
    }

    saveOnClick() {
        var self = this;
        $(".required input").map((index, item) => { $(item).focus(); $(item).blur(); });
        if ($(".required--error").length === 0) {
            var url = 'http://localhost:49779/api/Employees';
            var method;
            var data = {};
            data.EmployeeCode = $("#EmployeeCode").val();
            data.FullName = $("#FullName").val();
            data.IdentityNumber = $("#IdentityNumber").val();
            data.IdentityPlace = $("#IdentityPlace").val();
            data.Email = $("#Email").val();
            data.PhoneNumber = $("#PhoneNumber").val();
            data.PersonalTaxCode = $("#PersonalTaxCode").val();
            data.Salary = $("#Salary").val();
            data.Gender = $("#Gender").val();
            data.JobStatus = $("#JobStatus").val();
            data.PositionId = $("#PositionId").val();
            data.DepartmentId = $("#DepartmentId").val();
            data.JoinDate = $("#JoinDate").val();
            data.IdentityDate = $("#IdentityDate").val();
            data.DateOfBirth = $("#DateOfBirth").val();

            if (this.formMode === this.formModes.ADD) {
                method = 'POST';
            } else if (this.formMode === this.formModes.EDIT) {
                method = 'PUT';
                data.EmployeeId = $(".row--select").attr('id');
            }
            $.ajax({
                url: url,
                method: method,
                data: JSON.stringify(data),
                dataType: 'json',
                contentType: 'application/json'
            }).done(function (res) {
                console.log(res);
                if (res === -1) {
                    $('#EmployeeCode').parent().addClass("required--error");
                    $('#EmployeeCode').parent().attr("title", "Mã nhân viên đã tồn tại!");
                } else {
                    self.formMode = self.formModes.NONE;
                    self.hideDialogAdd();
                    self.loadData();
                }
            }).fail(function () {
                alert('Add or Edit: fail!');
            })
        } 
    }

    checkRequired(event) {
        if ($(event.target).val() === "") {
            $(event.target).parent().addClass("required--error");
            $(event.target).parent().attr("title", "Bạn phải nhập thông tin này!");
            return;
        }
        else {
            $(event.target).parent().removeClass("required--error");
            $(event.target).parent().removeAttr("title");
        }
        var filter;
        if ($(event.target).attr("id") === "Email") {
            filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            this.checkFormat(filter, event.target);
        } else if ($(event.target).attr("id") === "PhoneNumber" || $(event.target).attr("id") === "IdentityNumber") {
            filter = /^([0-9])/;
            this.checkFormat(filter, event.target);
        }
    }

    changePageOnClick(num) {
        this.currentPage = num;
        if (this.currentPage < 1) {
            this.currentPage = 1;
        }
        if (this.currentPage > this.pages) {
            this.currentPage = this.pages;
        }
        this.loadData();
    }

    addOnClick() {
        this.formMode = this.formModes.ADD;
        this.showDialogAdd();
        $.ajax({
            url: 'http://localhost:49779/api/employees/GetEmployeeCodeMax',
            method: 'GET',
            data: null,
            dataType: 'json',
            contentType: 'application/json'
        }).done(function (res) {
            $('#EmployeeCode').val('NV' + (new Number(res.slice(2, res.length)) + 1));
        }).fail(function (res) {
            $('#EmployeeCode').val('NV' + (new Number(res.responseText.slice(2, res.responseText.length)) + 1));
        });
    }

    cancelOnClick() {
        this.hideDialogAdd();
    }

    trOnClick() {
        var tr = event.target.parentElement;
        if ($('.row--select').length > 0 && $(tr).hasClass('row--select')) {
            $(tr).removeClass('row--select');
            $('.title__button .delete--employee').attr('disabled', true);
        } else {
            $('.row--select').map((index, element) => { $(element).removeClass("row--select") });
            $(tr).addClass('row--select');
            $('.title__button .delete--employee').attr('disabled', false);
        }
    }
    
    trOndblClick(event) {
        var self = this;
        $(event.target.parentElement).addClass('row--select');
        this.formMode = this.formModes.EDIT;
        this.showDialogAdd();
        var EmployeeId = $(".row--select").attr('id');
        $.ajax({
            url: 'http://localhost:49779/api/Employees/' + EmployeeId,
            method: 'GET',
            data: null,
            dataType: 'json',
            contentType: 'application/json'
        }).done(function (res) {
            $("#EmployeeCode").val(res.EmployeeCode);
            $("#FullName").val(res.FullName);
            $("#IdentityNumber").val(res.IdentityNumber);
            $("#IdentityPlace").val(res.IdentityPlace);
            $("#Email").val(res.Email);
            $("#PhoneNumber").val(res.PhoneNumber);
            $("#PersonalTaxCode").val(res.PersonalTaxCode);
            $("#Salary").val(res.Salary);
            $("#Gender").val(res.Gender);
            $("#JobStatus").val(res.JobStatus);
            $("#PositionId").val(res.PositionId);
            $("#DepartmentId").val(res.DepartmentId);
            if (res.JoinDate !== null) $("#JoinDate").val(self.FormatDate_Y_M_D(res.JoinDate));
            if (res.IdentityDate !== null) $("#IdentityDate").val(self.FormatDate_Y_M_D(res.IdentityDate));
            if (res.DateOfBirth !== null) $("#DateOfBirth").val(self.FormatDate_Y_M_D(res.DateOfBirth));
        }).fail(function () {
            alert('load employee by Id: fail!');
        })
    }

    showDialogAdd() {
        $("#dial").dialog("open");
        $("#dial .required--error").map((index, element) => { $(element).removeClass("required--error") });
        $('#dial input').map((index, element) => { $(element).val(""); });
    }

    hideDialogAdd() {
        $("#dial").dialog("close");
    }
}
