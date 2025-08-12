using Microsoft.AspNetCore.Mvc;
using Api_DGA.Application.Dtos.Common;
using APi_DGA_Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Api_DGA.Controllers
{
    /// <summary>
    /// Controlador para diagnóstico de la base de datos
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DiagnosticController : ControllerBase
    {
        private readonly InfrastructureContext _context;

        public DiagnosticController(InfrastructureContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene información de diagnóstico de la base de datos
        /// </summary>
        /// <returns>Información de diagnóstico</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponseDto<object>>> GetDiagnostic()
        {
            try
            {
                var diagnostic = new
                {
                    Timestamp = DateTime.UtcNow,
                    Database = new
                    {
                        Clients = await _context.Clients.CountAsync(),
                        Products = await _context.Products.CountAsync(),
                        Sales = await _context.Sales.CountAsync(),
                        SaleProducts = await _context.SaleProducts.CountAsync()
                    },
                    SampleData = new
                    {
                        FirstClient = await _context.Clients.FirstOrDefaultAsync(),
                        FirstProduct = await _context.Products.FirstOrDefaultAsync(),
                        FirstSale = await _context.Sales.FirstOrDefaultAsync(),
                        SalesWithClientId1 = await _context.Sales.Where(s => s.ClientId == 1).CountAsync()
                    }
                };

                return Ok(new ApiResponseDto<object>
                {
                    Success = true,
                    Data = diagnostic,
                    Message = "Información de diagnóstico obtenida"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto<object>
                {
                    Success = false,
                    Data = new { Error = ex.Message },
                    Message = "Error al obtener información de diagnóstico"
                });
            }
        }

        /// <summary>
        /// Obtiene todas las ventas con información del cliente
        /// </summary>
        /// <returns>Lista de ventas con información del cliente</returns>
        [HttpGet("sales-with-clients")]
        public async Task<ActionResult<ApiResponseDto<object>>> GetSalesWithClients()
        {
            try
            {
                var salesWithClients = await _context.Sales
                    .Include(s => s.Client)
                    .Select(s => new
                    {
                        s.Id,
                        s.Date,
                        s.Total,
                        ClientId = s.ClientId,
                        ClientName = s.Client.Name,
                        ClientEmail = s.Client.Email
                    })
                    .ToListAsync();

                return Ok(new ApiResponseDto<object>
                {
                    Success = true,
                    Data = salesWithClients,
                    Message = "Ventas con información de clientes obtenidas"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto<object>
                {
                    Success = false,
                    Data = new { Error = ex.Message },
                    Message = "Error al obtener ventas con clientes"
                });
            }
        }

        /// <summary>
        /// Verifica si existe un cliente específico
        /// </summary>
        /// <param name="clientId">ID del cliente</param>
        /// <returns>Información del cliente</returns>
        [HttpGet("client/{clientId}")]
        public async Task<ActionResult<ApiResponseDto<object>>> GetClientInfo(int clientId)
        {
            try
            {
                var client = await _context.Clients
                    .Include(c => c.Sales)
                    .ThenInclude(s => s.SaleProducts)
                    .ThenInclude(sp => sp.Product)
                    .FirstOrDefaultAsync(c => c.Id == clientId);

                if (client == null)
                {
                    return NotFound(new ApiResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = $"Cliente con ID {clientId} no encontrado"
                    });
                }

                var clientInfo = new
                {
                    client.Id,
                    client.Name,
                    client.Email,
                    client.Phone,
                    SalesCount = client.Sales.Count,
                    Sales = client.Sales.Select(s => new
                    {
                        s.Id,
                        s.Date,
                        s.Total,
                        Products = s.SaleProducts.Select(sp => new
                        {
                            SaleId = sp.SaleId,
                            ProductId = sp.ProductId,
                            sp.Quantity,
                            sp.UnitPrice,
                            Subtotal = sp.Quantity * sp.UnitPrice,
                            Product = new
                            {
                                sp.Product.Id,
                                sp.Product.Name,
                                sp.Product.Description,
                                sp.Product.Price,
                                sp.Product.Stock
                            }
                        }).ToList()
                    }).ToList()
                };

                return Ok(new ApiResponseDto<object>
                {
                    Success = true,
                    Data = clientInfo,
                    Message = "Información del cliente obtenida"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto<object>
                {
                    Success = false,
                    Data = new { Error = ex.Message },
                    Message = "Error al obtener información del cliente"
                });
            }
        }

        /// <summary>
        /// Obtiene todos los clientes con sus ventas y productos
        /// </summary>
        /// <returns>Lista de clientes con ventas y productos</returns>
        [HttpGet("all-clients-with-sales")]
        public async Task<ActionResult<ApiResponseDto<object>>> GetAllClientsWithSales()
        {
            try
            {
                var clientsWithSales = await _context.Clients
                    .Include(c => c.Sales)
                    .ThenInclude(s => s.SaleProducts)
                    .ThenInclude(sp => sp.Product)
                    .Select(c => new
                    {
                        c.Id,
                        c.Name,
                        c.Email,
                        c.Phone,
                        SalesCount = c.Sales.Count,
                        TotalSpent = c.Sales.Sum(s => s.Total),
                        Sales = c.Sales.Select(s => new
                        {
                            s.Id,
                            s.Date,
                            s.Total,
                            ProductsCount = s.SaleProducts.Count,
                            Products = s.SaleProducts.Select(sp => new
                            {
                                SaleId = sp.SaleId,
                                ProductId = sp.ProductId,
                                sp.Quantity,
                                sp.UnitPrice,
                                Subtotal = sp.Quantity * sp.UnitPrice,
                                Product = new
                                {
                                    sp.Product.Id,
                                    sp.Product.Name,
                                    sp.Product.Description,
                                    sp.Product.Price,
                                    sp.Product.Stock
                                }
                            }).ToList()
                        }).ToList()
                    })
                    .ToListAsync();

                return Ok(new ApiResponseDto<object>
                {
                    Success = true,
                    Data = clientsWithSales,
                    Message = "Todos los clientes con ventas y productos obtenidos"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto<object>
                {
                    Success = false,
                    Data = new { Error = ex.Message },
                    Message = "Error al obtener clientes con ventas"
                });
            }
        }
    }
}
