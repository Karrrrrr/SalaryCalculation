   M S B A M L   `   `   `  ????L  HSalaryCalculation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:SalaryCalculationSalaryCalculation  5   	       SalaryCalculation.Salary1   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:SalaryCalculation     5   	   -   5          ? Title$%  Заработная плата??5   	   $	??600??6"   $
??1000??6/   $	??600??6<   $
??1000??6L   .??5	      ]? .+?5
   
   ??  ?? Orientation$ 
Horizontal=?6   $	??Top=?6.   $??Right=?6C   .+?5      ?? -   5        $??20(?5      $	??200??6#   $??10q?6/   .??6P   Изменить6Z   ?? 5      -   5        $??20(?5      $	??200??6#   $??10q?6/   .??6R   Сохранить в pdf6c   5      ?? 5   
   $??1q?6   
???   ?._?5      ? -   5         ?Name$ 
SalaryGrid??5   (    ? ShowGridLines .  ? ColumnDefinitions 5      ?? 5      ?? 5      ?? 5      ?? 5      ?? 5       ? RowDefinitions 5      ?? 5      .+?5      ?? $??20(?6   $
??Bold"?6*    ?? TextAlignment$ Center=?6<   $??0??6S   $??0??6c   Работник6z   ?? 5      $??20(?6   $
??Bold"?6*   $ Center=?6<   $??1??6S   $??0??6c   Месяц и год6}   ?? 5      $??20(?6   $
??Bold"?6*   $ Center=?6<   $??2??6S   $??0??6c   Начислено6{   ?? 5      $??20(?6   $
??Bold"?6*   $ Center=?6<   $??3??6S   $??0??6c   Удержано6z   ?? 5      $??20(?6   $
??Bold"?6*   $ Center=?6<   $??4??6S   $??0??6c   Выплачено6{   5      5       5!      5"      