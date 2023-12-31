global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.EntityFrameworkCore.Design;
global using MediatR;
global using Microsoft.EntityFrameworkCore.Storage;
global using System.Data;
global using Microsoft.Extensions.Logging;
global using System.Reflection;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Hosting;
global using Fintranet.Services.CongestionTax.Infrastructure.Factories;
global using Microsoft.Extensions.Hosting;
global using FluentValidation;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using System.Net;
global using Fintranet.BuildingBlocks.Common.Domain.SeedWork;
global using Fintranet.Services.CongestionTax.Contracts.Interfaces;
global using Fintranet.BuildingBlocks.Common.Infrastructure.ErrorHandler;
global using Fintranet.Services.CongestionTax.Infrastructure.Data;
global using Fintranet.BuildingBlocks.Common.Infrastructure.ErrorHandler.Exceptions;
global using Fintranet.Services.CongestionTax.Infrastructure.Exceptions;
global using Fintranet.Services.CongestionTax.Domain.CityAggregate;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Fintranet.Services.CongestionTax.Infrastructure.EntityConfigurations;
global using Fintranet.BuildingBlocks.Common.SharedKernel.Extensions;
global using Fintranet.Services.CongestionTax.Domain.VehicleAggregate;
global using Fintranet.Services.CongestionTax.Domain.WorkingCalendarAggregate;
global using Fintranet.Services.CongestionTax.Infrastructure.Repositories;
global using Fintranet.Services.CongestionTax.Domain.Services;