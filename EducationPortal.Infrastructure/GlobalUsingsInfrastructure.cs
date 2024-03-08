// Project namespaces
global using EducationPortal.Domain.Models;
global using EducationPortal.Domain.Models.Materials;
global using EducationPortal.Application.Interfaces.Repositories;
global using EducationPortal.Infrastructure.DataAccess;
global using EducationPortal.Application.Interfaces.Services;
global using EducationPortal.Infrastructure.Repositories;
global using EducationPortal.Infrastructure.BackgroundJobs;
global using EducationPortal.Infrastructure.Services;

// 3rd party directives
global using Microsoft.EntityFrameworkCore;
global using System.Reflection;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using System.Linq.Expressions;
global using System.Security.Cryptography;
global using Konscious.Security.Cryptography;
global using Microsoft.Extensions.DependencyInjection;
global using Quartz;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.Logging;
global using RabbitMQ.Client;
global using Newtonsoft.Json;
global using System.Text;
global using Confluent.Kafka;