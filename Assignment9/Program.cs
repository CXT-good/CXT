using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OrderApp;

namespace OrderApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 创建 Web 应用程序的生成器实例
            var builder = WebApplication.CreateBuilder(args);

            // 配置数据库连接
            var connectionString = "Server=localhost;Database=OrderDataBase;User=root;password=cr3502427;";
            var serverVersion = ServerVersion.AutoDetect(connectionString);
            builder.Services.AddDbContext<OrderDbContext>(options =>
                options.UseMySql(connectionString, serverVersion));

            // 添加控制器服务，用于处理 API 控制器
            builder.Services.AddControllers();

            // 添加 Endpoint API 探索服务，为生成 API 文档做准备
            builder.Services.AddEndpointsApiExplorer();

            // 添加 Swagger 生成服务，用于生成 API 文档
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "OrderApp API", Version = "v1" });
            });

            // 添加自定义的 OpenAPI 配置服务（如果有的话）
            builder.Services.AddOpenApi();

            // 构建 Web 应用程序实例
            var app = builder.Build();

            // 配置 HTTP 请求管道，在开发环境下启用 OpenAPI 文档的映射
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderApp API V1");
                });
            }

            // 启用 HTTPS 重定向
            app.UseHttpsRedirection();

            // 启用授权中间件
            app.UseAuthorization();

            // 映射控制器，使 API 端点可用
            app.MapControllers();

            // 启动 Web 应用程序
            app.Run();
        }
    }
}
