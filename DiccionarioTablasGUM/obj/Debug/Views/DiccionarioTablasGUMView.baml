   M S B A M L   `   `   `  ����O  KDiccionarioTablasGUM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:DiccionarioTablasGUM.ViewsDiccionarioTablasGUM.Views  5   	   9    3DiccionarioTablasGUM.Views.DiccionarioTablasGUMView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:DiccionarioTablasGUM.Views     5   	   Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  5   	   $��Avenir Light)�5	   	   $��14(�5
   	     � Title$*  $Siesa - Diccionario de datos SIESAEE��5   	    � WindowStyle$
 None=�67    � Icon$" /Assets/iconoSiesaCompBD.ICO��6J    � WindowStartupLocation$ CenterScreen=�6n    � WindowState$ 	Maximized=�5   	    � 
ResizeMode$ CanResizeWithGrip=�6!   .��5      � ���	#FFE0E0E0 � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   � -   5        $��0��5      ���#0E4D96$��50��61   $	��Top=�6=    � ColumnDefinitions 5      �� 5      $��8 �6#   �� 5      $
��auto �6#   �� 5      $��* �6#   �� 5      $
��auto �6#   �� 5      $��8 �6#    5      �� 5      $��8 �6    �� 5      $��* �6    �� 5       $��8 �6    .+�5#      �� -   5         ��Name$  TituloDiccionarioTablasGum��5#      $"��DICCIONARIO DE DATOS SIESAEE��6<   	 �� TextAlignment$	 center=�6`   $��Center=�6w   ���#0E4D96$��Avenir LT Std 65 Medium)�5%      $��16(�6=   $
��Bold"�6K   
�������$��1��5&      $��1��5'      $	��211��5(      �� 5+      -   5        
 ��Name$
 ImagenLogoSiesa��5+      $!��/Assets/logoSiesaBlanco.png��5,      $��Center=�5-      $��Center=�5.      $
��auto��5/      $��1��50      $��2��51      � 54      $��3��6   $��1��6#   $	��211��60   .+�55      ��  �� Orientation$ 
Horizontal=�6   $��Right=�67   .+�56      �� -   5         ��Name$ btnMinimizar��56      $��Right=�6N   $��5q�6j   $��20��6u   
������ $��0q�6�   .��57      "  FontAwesome.WPF.ImageAwesome     Icon$ WindowMinimize=�6*   $��10��6@   $��10��6K      
Foreground
 �����6W   58      �� 5:      -   5        $ btnRestaurar��5:      $��Right=�6N   $��5q�6j   $��20��6u   
������ $��0q�6�   .��5;        $ WindowRestore=�6*   $��10��6?   $��10��6J   
 �����6V   5<      �� 5>      -   5        $ btnMaximizar��5>      $��Right=�6N   $��5q�6j   $��20��6u   
������ $��0q�6�   .��5?        $ WindowMaximize=�6*   $��10��6@   $��10��6K   
 �����6W   5@      �� 5B      -   5        $ 	btnCerrar��5B      $��Right=�6H   $��5q�6d   $��20��6o   
������ $��0q�6�   .��5C      �� 
�������$
��Bold"�67   x6M   5D      5E      5F      5G      � 5I   
   $��1��6    5J      �� 5K      $��8 �6#   �� 5L      $
��auto �6#   �� 5M      $
��auto �6#   �� 5N      $��* �6#   �� 5O      $
��auto �6#   �� 5P      $
��auto �6#   �� 5Q      $��8 �6#    5T      �� 5U      $
��auto �6    �� 5V      $
��auto �6    �� 5W      $��* �6    �� 5X      $
��auto �6    �� 5Y      $��8 �6    .+�5\      &   System.Windows.Controls.DataGrid  -   5          Name$ PubListTablasGum��5a         AutoGenerateColumns .     CanUserAddRows .  $��2��5^      $��1��5_      $��5��5`      $��0,8,0,0q�5d         HorizontalGridLinesBrush �#F0F0F0   VerticalGridLinesBrush �#F0F0F0 �� SelectedItem 5b      �� *PubObjTablaGumSeleccionada+   	CellStyle 5h      �� 5i       �� 
TargetType*  $System.Windows.Controls.DataGridCell"  6   .�5j      ��  �� Property � �� �� Value
 �����68   �� 5k       � ��$ 0q�6<    �� Triggers 5l      P� 5m       P� Property �@ 
IsReadOnly P� Value . . �5n   "   ��  � �� �#F7F7F76?   5o      P� 5p       �@ 
IsSelected . . �5q   "   ��  � �� �#85C1E96?   5r      5t         RowStyle 5v      �� 5w      )  #System.Windows.Controls.DataGridRow"  6   .�5x      ��  �@�ToolTip 5y      �� 5z   "   ��6,   �� *nombre+5|      5}         Columns 5      4  .System.Windows.Controls.DataGridTemplateColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6-      SortMemberPath$ 	indCambio��6:       
IsReadOnly  . !   CellTemplate! 5�      �� 5�      .R�5�   "   � .+�5�   &   �� 
������ $��0q�6F   .��5�   *     $ Circle=�6;   $��10��6I   $��10��6T   ��5�   *   �� 5�   .   "  64    5�   2   �� 5�   6   " �� Binding" 6B   �� *	indCambio+# �� Value# 06`   .a�5�   >   ��  �@ 
Foreground
 � � �6[   �� 5�   >    �@�ToolTip$* $Informaicion almacenada exitosamente��6X   5�   ;   �� 5�   6   " 6B   �� *	indCambio+# 16`   .a�5�   :   ��  �@ 
Foreground
 � ���6W   �� 5�   >    �@�ToolTip$ Informacion sin guardar��6X   5�   ;   �� 5�   6   " 6B   �� *
indEsNuevo+# 16a   .a�5�   :   ��  �@ 
Foreground
 �  ��6W   �� 5�   :    �@�ToolTip$ Objeto Nuevo��6T   5�   ;   5�   /   5�   +   5�   '   5�   #   5�      5�      0  *System.Windows.Controls.DataGridTextColumn  5�      $   Header$$ Nombre tabla��6)     . 1	  +System.Windows.Controls.DataGridBoundColumn% 	  Binding% 6?   �� *nombre+6\     5�      $$ Descripción��6)   % 6>   �� *descripcion+& �� UpdateSourceTrigger$& PropertyChanged=�'   	CellStyle' 5�      �� 5�      "  6$    5�   "   �� 5�   &   " 62   �� *descripcion+#  6R   .a�5�   *   ��  � �� �#EF274D6H   5�   '   5�      5�      4
  .System.Windows.Controls.DataGridCheckBoxColumn
  5�      $$ Permite GUM��6-   % 6C   �� *indProcesoGum+$& PropertyChanged=�
  5�      $$ Cambios��6-     . % 6>   �� *indCambioEnDB+6a   
  5�      $$ Campos Agregados��6-     . % 6G   �� *indCampoAgregado+6o   
  5�      $$ Campos Modificados��6-     . % 6I   �� *indCampoModificado+6s   
  5�      $$ Tabla virtual��6-     . % 6D   �� *indTablaVirtual+6k     5�      -	   5        
Cell_Notas��$$ Notas��5�   =   $	 500 6�   % 6L   �� *notas+$& PropertyChanged=�' 5�      �� 5�      "  6$    5�   "   �� 5�   &   " 62   �� *notas+#  6L   .a�5�   *   ��  � �� �#EF274D6H   5�   '   5�      5�        5�      $$ Acciones��6-   $
 Auto 6?   ! 5�      �� 5�      .R�5�   "   �� $ btnTablaCampos��6)   -
   5        
�������
������ $��0q�5�   )   ( � ToolTip$[( UEdicion de campos de la tablas que estan registradas en el Diccionario de tablas GUM.��5�   )   .��5�   &     $ Table=�66   $��10��6C   $��10��6N    �#0E4D966Z   5�   #   5�      5�      5�      �� 5�      -   5        $ btnActualizar��5�         BtnStiloBotones#���   5�      
�������$��21��5�      $��21��5�      $��3��5�      $��1��5�      $��0,13,8,5q�5�      $	��100��5�      $	��100��5�      $M( G Realiza actualizacion de todos los objetos nuevos en la base de datos ��5�      .��5�      �� $ 
Horizontal=�6   .+�5�        $ Refresh=�6&   $��10��65   $��10��6@   
 �����6L   �� 5�      $�� Actualizar��6    5�      5�      �� 5�      -   5        $ AbrirVentanaTablaVirtual��5�      #���   5�      
�������$��21��5�      $��21��5�      $��3��5�      $��2��5�      $��0,13,8,5q�5�      $	��150��5�      $
��Auto��5�      .��5�      �� $ 
Horizontal=�6   .+�5�        $
 Plus=�6&   $��10��62   $��10��6=   
 �����6I   �� 5�      $�� Crear tabla virtual��6    5�      5�      �� 5�      -   5        $ btnExportar��5�      
�������#���   5�      $��0q�5�      $��21��5�      $��3��5�      $��4��5�      $��0,8,8,0q�5�      $	��100��5�      $	��100��5�      ${( uExportar archivo con script para ejecutar en la base de datos de Oracle con el fin de actualizar los datos ingresados��5�      .��5�      �� $ 
Horizontal=�6   .+�5�        $ Download=�6&   $��10��66   $��10��6A   
 �����6M   �� 5�      $��	 Exportar��6    5�      5�      �� 5      -   5        $ btnConfirmarCambios��5      
�������#���   5     $��21��5     $	��100��5     $	��100��5     $��3��5     $��5��5     $��0,8,0,0q�5	     $H( BGuarda los cambios realizados sobre las tablas del diccionario GUM��5
     .��5     �� $ 
Horizontal=�6   .+�5       $
 Save=�6&   $��10��62   $��10��6=   
 �����6I   �� 5     $�� Guardar��6    5     5     5     5     5     