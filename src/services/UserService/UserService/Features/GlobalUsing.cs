﻿global using AutoMapper;
global using BC = BCrypt.Net.BCrypt;
global using Core;
global using Infrastructure.EFCore.Controllers;
global using Infrastructure.EFCore.Data;
global using Infrastructure.EFCore.DTOs;
global using Infrastructure.EFCore.Extensions;
global using Infrastructure.EFCore.Helpers;
global using Infrastructure.EFCore.Repository;
global using Infrastructure.EFCore.Service;
global using Constants = Infrastructure.Main.Constants;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.Extensions.Caching.Memory;
global using Microsoft.IdentityModel.Tokens;
global using OAuth2Service.Configurations;
global using OAuth2Service.DTOs;
global using OAuth2Service.Infrastructure.Data;
global using OAuth2Service.Models;
global using OAuth2Service.Profiles;
global using OAuth2Service.Repository;
global using OAuth2Service.Services;
global using System.ComponentModel.DataAnnotations;
global using System.IdentityModel.Tokens.Jwt;
global using System.Linq.Expressions;
global using System.Security.Claims;
global using System.Text;
global using System.Text.Json.Serialization;
global using RabbitMQ.Client;
