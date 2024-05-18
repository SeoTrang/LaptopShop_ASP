/*
<div class="profile-order">
    <nav>
        <div class="nav nav-tabs" id="nav-tab" role="tablist">
            <button class="nav-link active" id="nav-home-tab" data-bs-toggle="tab" data-bs-target="#nav-home" type="button" role="tab" aria-controls="nav-home" aria-selected="true">
                <span class="nav-name me-md-2 me-lg-2 me-xl-2">Tất cả</span>
                @* <span class="badge all">@Model.OrderList.Count</span> *@
            </button>
            <button class="nav-link" id="nav-profile-tab" data-bs-toggle="tab" data-bs-target="#nav-profile" type="button" role="tab" aria-controls="nav-profile" aria-selected="false">
                <span class="nav-name me-md-2 me-lg-2 me-xl-2">Chờ</span>
                @* <span class="badge pedding">@Model.OrderList.Count(o => o.State == "pending")</span> *@
            </button>
            <button class="nav-link" id="nav-contact-tab" data-bs-toggle="tab" data-bs-target="#nav-contact" type="button" role="tab" aria-controls="nav-contact" aria-selected="false">
                <span class="nav-name me-md-2 me-lg-2 me-xl-2">Đang giao</span>
                @* <span class="badge delivering">@Model.OrderList.Count(o => o.State == "shipping")</span> *@
            </button>
            <button class="nav-link" id="nav-disabled-tab" data-bs-toggle="tab" data-bs-target="#nav-disabled" type="button" role="tab" aria-controls="nav-disabled" aria-selected="false">
                <span class="nav-name me-md-2 me-lg-2 me-xl-2">Đã giao</span>
                @* <span class="badge completed">@Model.OrderList.Count(o => o.State == "success")</span> *@
            </button>
            <button class="nav-link" id="nav-cancelled-tab" data-bs-toggle="tab" data-bs-target="#nav-cancelled" type="button" role="tab" aria-controls="nav-cancelled" aria-selected="false">
                <span class="nav-name me-md-2 me-lg-2 me-xl-2">Đã hủy</span>
                @* <span class="badge cancelled">@Model.OrderList.Count(o => o.State == "canceled")</span> *@
            </button>
        </div>
    </nav>
    <div class="tab-content mt-3" id="nav-tabContent">
        <div class="tab-pane fade show active" id="nav-home" role="tabpanel" aria-labelledby="nav-home-tab" tabindex="0">
            <div class="content-order">
                @if (Model.OrderList.Count > 0)
                {
                    @foreach (var order in Model.OrderList)
                    {
                        <a href="@Url.Action("OrderDetail", new { id = order.Id })" class="row row-order mb-3">
                            <div class="col-2 code-id">@order.Code</div>
                            <div class="col-4 col-md-3 col-lg-3 col-xl-3">@order.TotalPrice.ToString("C")</div>
                            @* <div class="col-3 col-md-3 col-lg-3 col-xl-3">@order.CreatedAt.ToString("dd/MM/yyyy")</div> *@
                            <div class="col-3">
                                @if (order.State == "pending")
                                {
                                    <span class="pedding">Chờ</span>
                                }
                                else if (order.State == "shipping")
                                {
                                    <span class="delivering">Đang giao</span>
                                }
                                else if (order.State == "success")
                                {
                                    <span class="completed">Đã giao</span>
                                }
                                else if (order.State == "canceled")
                                {
                                    <span class="cancelled">Đã hủy</span>
                                }
                            </div>
                            <div class="col-1 d-none d-md-block d-lg-block d-xl-block">
                                <span class="action-row"><i class="fa-solid fa-arrow-right"></i></span>
                            </div>
                        </a>
                    }
                }
                else
                {
                    <span>Bạn chưa có đơn hàng nào</span>
                }
            </div>
        </div>
        <div class="tab-pane fade" id="nav-profile" role="tabpanel" aria-labelledby="nav-profile-tab" tabindex="0">
            <div class="content-order">
                @foreach (var order in Model.OrderList.Where(o => o.State == "pending"))
                {
                    <a href="@Url.Action("OrderDetail", new { id = order.Id })" class="row row-order mb-3">
                        <div class="col-2 code-id">@order.Code</div>
                        <div class="col-4 col-md-3 col-lg-3 col-xl-3">@order.TotalPrice.ToString("C")</div>
                        @* <div class="col-3 col-md-3 col-lg-3 col-xl-3">@order.CreatedAt.ToString("dd/MM/yyyy")</div> *@
                        <div class="col-3"><span class="pedding">Chờ</span></div>
                        <div class="col-1 d-none d-md-block d-lg-block d-xl-block"><span class="action-row"><i class="fa-solid fa-arrow-right"></i></span></div>
                    </a>
                }
            </div>
        </div>
        <div class="tab-pane fade" id="nav-contact" role="tabpanel" aria-labelledby="nav-contact-tab" tabindex="0">
            <div class="content-order">
                @foreach (var order in Model.OrderList.Where(o => o.State == "shipping"))
                {
                    <a href="@Url.Action("OrderDetail", new { id = order.Id })" class="row row-order mb-3">
                        <div class="col-2 code-id">@order.Code</div>
                        <div class="col-4 col-md-3 col-lg-3 col-xl-3">@order.TotalPrice.ToString("C")</div>
                        @* <div class="col-3 col-md-3 col-lg-3 col-xl-3">@order.CreatedAt.ToString("dd/MM/yyyy")</div> *@
                        <div class="col-3"><span class="delivering">Đang giao</span></div>
                        <div class="col-1 d-none d-md-block d-lg-block d-xl-block"><span class="action-row"><i class="fa-solid fa-arrow-right"></i></span></div>
                    </a>
                }
            </div>
        </div>
        <div class="tab-pane fade" id="nav-disabled" role="tabpanel" aria-labelledby="nav-disabled-tab" tabindex="0">
            <div class="content-order">
                @foreach (var order in Model.OrderList.Where(o => o.State == "success"))
                {
                    <a href="@Url.Action("OrderDetail", new { id = order.Id })" class="row row-order mb-3">
                        <div class="col-2 code-id">@order.Code</div>
                        <div class="col-4 col-md-3 col-lg-3 col-xl-3">@order.TotalPrice.ToString("C")</div>
                        @* <div class="col-3 col-md-3 col-lg-3 col-xl-3">@order.CreatedAt.ToString("dd/MM/yyyy")</div> *@
                        <div class="col-3"><span class="completed">Đã giao</span></div>
                        <div class="col-1 d-none d-md-block d-lg-block d-xl-block"><span class="action-row"><i class="fa-solid fa-arrow-right"></i></span></div>
                    </a>
                }
            </div>
        </div>
        <div class="tab-pane fade" id="nav-cancelled" role="tabpanel" aria-labelledby="nav-cancelled-tab" tabindex="0">
            <div class="content-order">
                @foreach (var order in Model.OrderList.Where(o => o.State =="canceled"))
                {
                    <a href="@Url.Action("OrderDetail", new { id = order.Id })" class="row row-order mb-3">
                        <div class="col-2 code-id">@order.Code</div>
                        <div class="col-4 col-md-3 col-lg-3 col-xl-3">@order.TotalPrice.ToString("C")</div>
                        @* <div class="col-3 col-md-3 col-lg-3 col-xl-3">@order.CreatedAt.ToString("dd/MM/yyyy")</div> *@
                        <div class="col-3"><span class="cancelled">Đã hủy</span></div>
                        <div class="col-1 d-none d-md-block d-lg-block d-xl-block"><span class="action-row"><i class="fa-solid fa-arrow-right"></i></span></div>
                    </a>
                }
            </div>
        </div>
    </div>
</div>

*/