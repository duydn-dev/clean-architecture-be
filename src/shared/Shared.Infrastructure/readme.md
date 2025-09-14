Tầng Infrastructure dùng chung
- Chứa các service cung cấp chức năng kỹ thuật, hỗ trợ cho domain/application, ví dụ: gửi email, gửi SMS, lưu file, truy cập API bên ngoài, logging, caching, gửi message qua queue, v.v.
Chúng không chứa logic nghiệp vụ (business logic), mà chỉ thực hiện các tác vụ kỹ thuật.
- Chứa các code không phụ thuộc vào domain/business logic của từng microservice, mà chỉ là các tiện ích hạ tầng, cấu hình, extension methods, middleware, v.v.