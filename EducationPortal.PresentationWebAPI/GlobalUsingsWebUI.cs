// Project namespaces
global using EducationPortal.Application.Interfaces.Services;
global using EducationPortal.Domain.Entities;
global using EducationPortal.Application.DTOs.Authentication;
global using EducationPortal.Domain.Entities.Materials;
global using EducationPortal.Application.DTOs.User;
global using EducationPortal.PresentationWebAPI.Common;
global using EducationPortal.PresentationWebAPI.Validation.Validators.CustomValidatiors;
global using EducationPortal.Application.DTOs.Course;
global using EducationPortal.Application.DTOs.Skill;
global using EducationPortal.Application.DTOs.Material;
global using EducationPortal.Application.Common.Validation;
global using EducationPortal.PresentationWebAPI.Validation.Validators;
global using EducationPortal.Infrastructure;
global using EducationPortal.PresentationWebAPI.Middlewares;
global using EducationPortal.PresentationWebAPI.Validation;
global using EducationPortal.Application;

// 3rd party directives
global using Microsoft.Extensions.Options;
global using Swashbuckle.AspNetCore.SwaggerGen;
global using Microsoft.OpenApi.Models;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.IdentityModel.Tokens;
global using System.Text;
global using FluentValidation;
global using ValidationFailure = FluentValidation.Results.ValidationFailure;
global using Microsoft.AspNetCore.Mvc.Filters;
global using System.Text.RegularExpressions;
global using System.Net;
global using System.Text.Json;