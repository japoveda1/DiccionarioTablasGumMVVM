   M S B A M L   `   `   `  ���� O  KDiccionarioTablasGUM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:DiccionarioTablasGUM.ViewsDiccionarioTablasGUM.Views  1    +DiccionarioTablasGUM.Views.TablaVirtualView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     :local(clr-namespace:DiccionarioTablasGUM.Views     Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  -   $��Avenir Light)�$��14(�  � Title$*  $Siesa - Diccionario de datos SIESAEE��$	��195��$	��400�� � 
ResizeMode$ CanResizeWithGrip=� � AllowsTransparency .  � WindowStyle$
 None=� � Icon$" /Assets/iconoSiesaCompBD.ICO�� � WindowStartupLocation$ CenterScreen=�.��� .+�� ���	#FFE0E0E0 � RowDefinitions �� $
��auto ��� $��* �.+�� -   $��0�����#0E4D96$��50��$	��Top=� � ColumnDefinitions �� $��8 ��� $
��auto ��� $��* ��� $
��auto ��� $��8 � �� $��8 ��� $��* ��� $��8 �.+��� -    ��Name$ ImagenLogoSiesa��$!��/Assets/logoSiesaBlanco.png��$��Center=�$
��Left=�$
��auto��$��2��$��1���� -   	 ��Name$	 	btnCerrar��$��3��$��1��$��Right=�$��5q�$��20��
������ $��0q�.���� 
�������$
��Bold"�x� $��1�� �� $��8 ��� $��* ��� $��8 � �� $��8 ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* ��� $��8 �.+��� $��1��$��1��$��Tabla origen:���� -   $��cboTablasGUM��$��2��$��1������ 
 �� SelectedItem
 �� *PubListTablasGumSeleccionada+���� .R�$� �� $��3��$��1��$��Nombre tabla virtual:���� -    ��Name$ PubNombreTablaVirtual��$��6,0,0,0q�$��4��$��1��$��21���� -   $	 btnConfirmarCambios��
�������   BtnStiloBotones#���   $��21��$	��100��$	��100��$��5��$��1��$��0,8,0,0q�$��Right=� � ToolTip$@ :Guarda los cambios realizados sobre los campos de la Tabla��.����  �� Orientation$ 
Horizontal=�.+�"  FontAwesome.WPF.ImageAwesome     Icon$ Check=�$��10��$��10��   
Foreground
 ������� $�� Crear��