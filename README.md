Quy ước trình bày nội dung
============
1. Giữa các trường control có Spacing là 1 nếu sử dụng MudGrid.
2. Các Table và DataGrid sử dụng thuộc tính Dense, các thành phần bên trong đều phải định nghĩa padding là 1. Mục đích là 
để tối ưu cho giao diện điện thoại.
3. Cỡ chữ 16px.
4. Không được xuống dòng trong bảng, các cột liên quan đến tiền căn phải còn các cột liên quan đến số lượng căn giữa.

- - -
Khi tạo một menu yêu cầu quyền truy cập
============
Khi tạo một menu yêu cầu đăng nhập để nhìn thấy, cần bỏ vào trong <Authorized>.

Ví dụ:

    <Authorized>
        <MudNavGroup Title="Hệ thống" Icon="@Icons.Material.Filled.Apps" Expanded="false">
            <MudNavLink Href="/Hethong/Taikhoansudung" Icon="@Icons.Material.Filled.AccountBox">Tài khoản sử dụng</MudNavLink>
        </MudNavGroup>
    </Authorized>
    
---
Khi tạo một trang yêu cầu quyền truy cập
============
Menu dẫn đến trang yêu cầu quyền truy cập không có nghĩa là trang cũng yêu cầu quyền truy cập. Người dùng vẫn có thể truy cập trái phép bằng cách 
sử dụng đường dẫn trực tiếp đến trang. Để tránh trường hợp này xảy ra thì ta cần thêm @attribute [Authorize] vào đầu trang cần bảo vệ.

Ví dụ:

    @page "/Baocao/Baocaoxuathang/Tonghopbanle"

    @using Microsoft.AspNetCore.Authorization
    
    @attribute [Authorize]
    
    <MudText>Trang này cần đăng nhập mới truy cập được</MudText>
