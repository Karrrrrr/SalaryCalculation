   M S B A M L   `   `   `  ????L  HSalaryCalculation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:SalaryCalculationSalaryCalculation  5   	       SalaryCalculation.Workers   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:SalaryCalculation     5   	   -   5          ? Title$  Сотрудники??5   	   $	??450??6   $	??800??6)   $	??450??65   $	??800??6E   .??5	      ? -   5        $??WorkersGrid??5	        ? ShowGridLines .  ? ColumnDefinitions 5
   
   ?? 5      ?? 5      ?? 5      ?? 5       ? RowDefinitions 5   
   ?? 5      .+?5   
   ?? $??20(?6   $
??Bold"?6"    ?? TextAlignment$ Center=?64   $??0??6K   $??0??6[   ФИО6m   ?? 5   
   $??20(?6   $
??Bold"?6"   $ Center=?64   $??1??6K   $??0??6[   Должность6s   ?? 5   
   $??20(?6   $
??Bold"?6"   $ Center=?64   $??2??6K   $??0??6[   Разряд6p   ?? 5   
   $??20(?6   $
??Bold"?6"   $ Center=?64   $??3??6K   $??0??6[   $
??Wrap=?6h   (&Членство в профсоюзе6?   5      5      