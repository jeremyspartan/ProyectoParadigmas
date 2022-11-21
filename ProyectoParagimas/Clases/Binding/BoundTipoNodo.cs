namespace ProyectoParadigmas.Clases.Binding
{
    internal enum BoundTipoNodo
    {
        //Expresiones
        EXPRESION_LITERAL,
        EXPRESION_UNARIA,
        EXPRESION_BINARIA,
        EXPRESION_VARIABLE,
        EXPRESION_ASIGNACION,
        EXPRESION_ERROR,

        //Declaraciones
        BLOQUE_DECLARACION,
        VARIABLE_DECLARACION,
        EXPRESION_DECLARACION,
        IF_DECLARACION,
        WHILE_DECLRACION,
        FOR_DECLRARACION,
        GOTO_DECLARACION,
        LABEL_DECLRARACION,
        GOTO_CONDICIONAL_DECLARACION
    }
}
