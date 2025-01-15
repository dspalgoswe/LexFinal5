﻿using AutoMapper;
using Domain.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services;
public class ServiceManager : IServiceManager
{

    private readonly Lazy<IAuthService> authService;
    private readonly Lazy<ITeacherService> teacherService;
    public IAuthService AuthService => authService.Value;
    public ITeacherService TeacherService => teacherService.Value;
    public ServiceManager(Lazy<IAuthService> authService, Lazy<ITeacherService> teacherService)
    {
        this.authService = authService;
        this.teacherService = teacherService;
    }
}
