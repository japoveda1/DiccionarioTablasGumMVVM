   M S B A M L   `   `   `  ����O  KDiccionarioTablasGUM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:DiccionarioTablasGUM.ViewsDiccionarioTablasGUM.Views  5   	   1    +DiccionarioTablasGUM.Views.TablaVirtualView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:DiccionarioTablasGUM.Views     5   	   Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  5   	   -   5        $��Avenir Light)�5	   	   $��14(�5
   	     � Title$*  $Siesa - Diccionario de datos SIESAEE��5   	   $	��195��5   	   $	��400��5   	    � 
ResizeMode$ CanResizeWithGrip=�5   	    � AllowsTransparency .  � WindowStyle$
 None=�5   	    � Icon$" /Assets/iconoSiesaCompBD.ICO��5   	    � WindowStartupLocation$ CenterScreen=�5   	   .��5      � .+�5   
   � ���	#FFE0E0E0 � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $��* �6    .+�5      � -   5        $��0��5      ���#0E4D96$��50��65   $	��Top=�6A    � ColumnDefinitions 5      �� 5      $��8 �6'   �� 5       $
��auto �6'   �� 5!      $��* �6'   �� 5"      $
��auto �6'   �� 5#      $��8 �6'    5&      �� 5'      $��8 �6$   �� 5(      $��* �6$   �� 5)      $��8 �6$   .+�5,      �� -   5         ��Name$ ImagenLogoSiesa��5,      $!��/Assets/logoSiesaBlanco.png��5-      $��Center=�5.      $
��Left=�5/      $
��auto��50      $��2��51      $��1��52      �� 54      -   5        	 ��Name$	 	btnCerrar��55   %   $��3��54      $��1��55      $��Right=�6P   $��5q�6l   $��20��6w   
������ $��0q�6�   .��56      �� 
�������$
��Bold"�63   x6I   57      58      � 5:      $��1��6    5;      �� 5<      $��8 �6'   �� 5=      $��* �6'   �� 5>      $��8 �6'    5A      �� 5B      $��8 �6$   �� 5C      $
��Auto �6$   �� 5D      $
��Auto �6$   �� 5E      $
��Auto �6$   �� 5F      $
��Auto �6$   �� 5G      $
��Auto �6$   �� 5H      $��* �6$   �� 5I      $��8 �6$   .+�5L      �� $��1��6   $��1��6%   $��Tabla origen:��65   �� 5M      -   5        $��cboTablasGUM��5M      $��2��6G   $��1��6T   ��6/   �� 
 �� SelectedItem
 6d   �� *PubListTablasGumSeleccionada+��5N      �� 5O      .R�5P      $� 5Q      5S      �� 5U      $��3��6   $��1��6%   $��Nombre tabla virtual:��65   �� 5V      -   5         ��Name$ PubNombreTablaVirtual��5V      $��6,0,0,0q�6:   $��4��6K   $��1��6X   $��21��6h   �� 5X      -   5        $	 btnConfirmarCambios��5X      
�������   BtnStiloBotones#���   5[      $��21��5\      $	��100��5]      $	��100��5^      $��5��5_      $��1��5`      $��0,8,0,0q�5a      $��Right=�5b       � ToolTip$@ :Guarda los cambios realizados sobre los campos de la Tabla��5c      .��5d      ��  �� Orientation$ 
Horizontal=�6!   .+�5e      "  FontAwesome.WPF.ImageAwesome     Icon$ Check=�6*   $��10��67   $��10��6B      
Foreground
 �����6N   �� 5f      $�� Crear��6$   5g      5h      5i      5j      5l      5m      