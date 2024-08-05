﻿using FrontEnd.Models;

namespace FrontEnd.Helpers.Intefaces
{
    public interface IMazoHelper
    {
        List<MazoViewModel> GetMazos();
        MazoViewModel GetMazo(int id);
        MazoViewModel Add(MazoViewModel mazo);
        MazoViewModel Remove(int id);
        MazoViewModel Update(MazoViewModel mazo);
    }
}
