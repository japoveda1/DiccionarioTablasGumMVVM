   M S B A M L   `   `   `  ���� O  KDiccionarioTablasGUM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:DiccionarioTablasGUM.ViewsDiccionarioTablasGUM.Views  9    3DiccionarioTablasGUM.Views.DiccionarioTablasGUMView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     :local(clr-namespace:DiccionarioTablasGUM.Views     Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  $��Avenir Light)�$��14(�  � Title$*  $Siesa - Diccionario de datos SIESAEE�� � WindowStyle$
 None=� � Icon$" /Assets/iconoSiesaCompBD.ICO�� � WindowStartupLocation$ CenterScreen=� � WindowState$ 	Maximized=� � 
ResizeMode$ CanResizeWithGrip=�.��� ���	#FFE0E0E0 � RowDefinitions �� $
��auto ��� $��* �.+�� -   $��0�����#0E4D96$��50��$	��Top=� � ColumnDefinitions �� $��8 ��� $
��auto ��� $��* ��� $
��auto ��� $��8 � �� $��8 ��� $��* ��� $��8 �.+��� -    ��Name$  TituloDiccionarioTablasGum��$"��DICCIONARIO DE DATOS SIESAEE��	 �� TextAlignment$	 center=�$��Center=����#0E4D96$��Avenir LT Std 65 Medium)�$��16(�$
��Bold"�
�������$��1��$��1��$	��211���� -   
 ��Name$
 ImagenLogoSiesa��$!��/Assets/logoSiesaBlanco.png��$��Center=�$��Center=�$
��auto��$��1��$��2��� $��3��$��1��$	��211��.+���  �� Orientation$ 
Horizontal=�$��Right=�.+��� -    ��Name$ btnMinimizar��$��Right=�$��5q�$��20��
������ $��0q�.��"  FontAwesome.WPF.ImageAwesome     Icon$ WindowMinimize=�$��10��$��10��   
Foreground
 ������� -   $ btnRestaurar��$��Right=�$��5q�$��20��
������ $��0q�.��  $ WindowRestore=�$��10��$��10��
 ������� -   $ btnMaximizar��$��Right=�$��5q�$��20��
������ $��0q�.��  $ WindowMaximize=�$��10��$��10��
 ������� -   $ 	btnCerrar��$��Right=�$��5q�$��20��
������ $��0q�.���� 
�������$
��Bold"�x� $��1�� �� $��8 ��� $
��auto ��� $
��auto ��� $��* ��� $
��auto ��� $
��auto ��� $��8 � �� $
��auto ��� $
��auto ��� $��* ��� $
��auto ��� $��8 �.+�&   System.Windows.Controls.DataGrid  -     Name$ PubListTablasGum��   AutoGenerateColumns .     CanUserAddRows .  $��2��$��1��$��5��$��0,8,0,0q�   HorizontalGridLinesBrush �#F0F0F0   VerticalGridLinesBrush �#F0F0F0 �� SelectedItem �� *PubObjTablaGumSeleccionada+   	CellStyle ��  �� 
TargetType*  $System.Windows.Controls.DataGridCell"  .���  �� Property � �� �� Value
 �������  � ��$ 0q� �� Triggers P�  P� Property �@ 
IsReadOnly P� Value . . ���  � �� �#F7F7F7P�  �@ 
IsSelected . . ���  � �� �#85C1E9   RowStyle �� )  #System.Windows.Controls.DataGridRow"  .���  �@�ToolTip �� ���� *nombre+   Columns 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto    SortMemberPath$ 	indCambio��    
IsReadOnly  . !   CellTemplate! �� .R�� .+��� 
������ $��0q�.��  $ Circle=�$��10��$��10������ "   �� " �� Binding" �� *	indCambio+# �� Value# 0.a���  �@ 
Foreground
 � � ���  �@�ToolTip$* $Informaicion almacenada exitosamente���� " �� *	indCambio+# 1.a���  �@ 
Foreground
 � �����  �@�ToolTip$ Informacion sin guardar���� " �� *
indEsNuevo+# 1.a���  �@ 
Foreground
 �  ����  �@�ToolTip$ Objeto Nuevo��0  *System.Windows.Controls.DataGridTextColumn  $   Header$$ Nombre tabla��  . 1	  +System.Windows.Controls.DataGridBoundColumn% 	  Binding% �� *nombre+  $$ Descripción��% �� *descripcion+& �� UpdateSourceTrigger$& PropertyChanged=�'   	CellStyle' �� "   �� " �� *descripcion+#  .a���  � �� �#EF274D4
  .System.Windows.Controls.DataGridCheckBoxColumn
  $$ Permite GUM��% �� *indProcesoGum+$& PropertyChanged=�
  $$ Cambios��  . % �� *indCambioEnDB+
  $$ Campos Agregados��  . % �� *indCampoAgregado+
  $$ Campos Modificados��  . % �� *indCampoModificado+
  $$ Tabla virtual��  . % �� *indTablaVirtual+  -	   
Cell_Notas��$$ Notas��$	 500 % �� *notas+$& PropertyChanged=�' �� "   �� " �� *notas+#  .a���  � �� �#EF274D  $$ Acciones��$
 Auto ! �� .R��� $ btnTablaCampos��-
   
�������
������ $��0q�( � ToolTip$[( UEdicion de campos de la tablas que estan registradas en el Diccionario de tablas GUM.��.��  $ Table=�$��10��$��10�� �#0E4D96�� -   $ btnActualizar��   BtnStiloBotones#���   
�������$��21��$��21��$��3��$��1��$��0,13,8,5q�$	��100��$	��100��$M( G Realiza actualizacion de todos los objetos nuevos en la base de datos ��.���� $ 
Horizontal=�.+�  $ Refresh=�$��10��$��10��
 ������� $�� Actualizar���� -   $ AbrirVentanaTablaVirtual��#���   
�������$��21��$��21��$��3��$��2��$��0,13,8,5q�$	��150��$
��Auto��.���� $ 
Horizontal=�.+�  $
 Plus=�$��10��$��10��
 ������� $�� Crear tabla virtual���� -   $ btnExportar��
�������#���   $��0q�$��21��$��3��$��4��$��0,8,8,0q�$	��100��$	��100��${( uExportar archivo con script para ejecutar en la base de datos de Oracle con el fin de actualizar los datos ingresados��.���� $ 
Horizontal=�.+�  $ Download=�$��10��$��10��
 ������� $��	 Exportar���� -   $ ConfirmarCambios��
�������#���   $��21��$	��100��$	��100��$��3��$��5��$��0,8,0,0q�$H( BGuarda los cambios realizados sobre las tablas del diccionario GUM��.���� $ 
Horizontal=�.+�  $
 Save=�$��10��$��10��
 ������� $�� Guardar��