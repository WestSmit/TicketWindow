using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Infrastructure;
using AutoMapper;
using MVC.Models;
using System.Globalization;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        IRouteService RouteService;
        public HomeController(IRouteService service)
        {
            RouteService = service;
        }
        public ActionResult Index()
        {
            RouteDTO routeDTO = RouteService.GetRoute(1);
            var RouteMapper = new MapperConfiguration(cfg => cfg.CreateMap<RouteDTO, RouteViewModel>()).CreateMapper();
            ViewBag.Route = RouteMapper.Map<RouteDTO, RouteViewModel>(routeDTO);
            return View();
        }

        
        [HttpPost]
        public ActionResult Search(string FromStation, string ToStation, string date)
        {
            DateTime Date = DateTime.Parse(date);
            List<RouteDTO> RoutesDTO = RouteService.SearchRoute(FromStation, ToStation, Date);
          
            var RouteListMapper = new MapperConfiguration(cfg => { cfg.CreateMap<RouteDTO, RouteViewModel>();
                cfg.CreateMap<CarriageDTO, CarriageViewModel>();
            }).CreateMapper();            
            List<RouteViewModel> FoundRoutes = RouteListMapper.Map<List<RouteDTO>, List<RouteViewModel>>(RoutesDTO);
                        
            ResultOfSearchViewModel Result = new ResultOfSearchViewModel();
            Result.FoundRoutes = FoundRoutes;
            Result.From = FromStation;
            Result.To = ToStation;
            Result.Date = Date;
            
            return View(Result);
        }
        

    }
}