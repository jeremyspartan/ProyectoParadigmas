   M S B A M L   `   `   `  ????M  IProyectoParadigmas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ProyectoParadigmasProyectoParadigmas  5   	   #    ProyectoParadigmas.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:ProyectoParadigmas     5   	     ? Title$  Proyecto Paradigmas??5      $	??783??6$   $	??800??61   .??5	      ? $	??702??6   $	??Top=?6    ? ColumnDefinitions 5
   
   ?? 5      $
??407* ?6    ? RowDefinitions 5   
   ?? 5      $	??43* ?6   ?? 5      $
??404* ?6   ?? 5      $	??50* ?6   ?? 5      $	??205 ?6   .+?5   
   o? -   5         o?Name$ 
menu_tools??5      $??	10,9,10,9q?6#   .@?5      ?? -   5         ??Name$ btn_compilar??5      $??Compilar??6+   $??Center=?6>   $??Bottom=?6[   6v   ?? 5      -   5        $ btn_ejecutar??5      $??Ejecutar??6+   $??Center=?6>   $??Center=?6[   6v   5      o? 5   
   -   5        $ 	menu_info??5      $??	10,0,10,0q?6"   $??2??65   $??30??6B   $??Center=?6N   .@?5      ?? -   5         ??Name$ 	lbl_lines??5      $??
Líneas: 1??6'   $
??Left=?6;   $	??Top=?6V   ?? 5      -   5        $ lbl_line??5      $??	Línea: 1??6&   $
??Left=?69   $	??Top=?6T   ?? 5      -   5        $ lbl_col??5      $??
Columna: 1??6%   $
??Left=?6:   $	??Top=?6U   5      ?? 5   
   -   5         ??Name$ 
Rtb_Codigo??5      $??10,34,10,40q?6*   $??Arial)?6]   ???	#FF1E1E1E???	#FF569CD6$??3??6?   . ?5      /? .T?5      J?  ?? 
LineHeight$ 1??6   .*?6+   5       5!      5"      ?? 5#   
   -	   5        $ 
Rtb_OutPut??5#      $??10,10,10,10q?6*    ?? 
IsReadOnly$
 True??6?   $??Arial)?6v   ???	#FF1E1E1E???	#FF569CD6$??3??6?   . ?5$      /? .T?5%      J? $ 1??6   5&      5'      5)      5*      