using System;
using System.Collections.Generic;
using System.Linq;


namespace Ejemplo1
{
    class Sintaxis
    {
        #region Encontrar Valor
        int contadorIF = -1;
        bool activaoElse = false;
        int contador = 0;
        ObjPolish ob = new ObjPolish();
        public static List<ObjPolish> _polish = new List<ObjPolish>();
        List<Identificadores> _identificadoreses = new List<Identificadores>();
        Queue<Struct> queueSolucions = new Queue<Struct>();
        Stack<Struct> stackAnalisis = new Stack<Struct>();
        Stack<Struct> soluciStack  = new Stack<Struct>();
        #endregion

        private bool activaif = false;
        private bool activaelse = false;
        private int _controlvar = 0;
        #region  asigancion identificadores
        private Queue<int> operacionQueue = new Queue<int>(); 
        private static List<string> _idenuso = new List<string>() ;
      
#endregion

        static List<Struct> _structs = new List<Struct>();
        public static readonly List<string> errores = new List<string>();
        static readonly String[] _error = new string[]
        {
            "Se esperaba un End en la linea: ",
            "Se esperaba la palabra reservada Then en la linea: ",
            "Se esperaba un = en la linea: ",
            "No se declara el identificador en la linea ",
            "Se esperaba un parentesis en la linea: ",
            "Error de sintaxis en la linea: ",
            "Falta una expresion en la linea: "
        };

        static readonly String[] _errorSemantica = new string[]
        {
            
            "stdin:1: attempt to perform arithmetic on a string value",
            "stdin:1: attempt to perform arithmetic on a boolean value",
            "stdin:1: malformed number near",
            "stdin:1: attempt to concatenate a boolean value"
        };
        #region Getter & Setter
        public List<Struct> Strucs
        {
            get { return _structs; }
            set { _structs = value; }
        }

        public List<Identificadores> Identificadoreses
        {
            get { return _identificadoreses; }
            set { _identificadoreses = value; }
        }

        public List<string> Errores()
        {
            return errores;
        }
        public List<ObjPolish> Polish
        {
            get { return _polish; }
        }
        
#endregion

        static int p= 0;
        public void Inicializa()
        {
            errores.Clear();
            p = 0;
            while (p < _structs.Count)
            {
                Sentencia();
            }
        }
        private void Chunk() => Sentencia();

        internal void Bloque() => Chunk();

        public void Sentencia(){

            if (p < _structs.Count)
            {
                if (esIf(_structs[p].token))
                {
                    contadorIF++;
                    p++;
                    if (Metodoexp())
                    {
                        while (stackAnalisis.Count > 0)
                        {
                            queueSolucions.Enqueue(stackAnalisis.Pop());
                        }
                        AsignarValor();
                        soluciStack.Clear();
                        _polish.Add(new ObjPolish
                        {
                            operador = "BRF",
                            resultado = "Q" + contadorIF,
                            operador1 = "T"+contador
                        });
                        if (p < _structs.Count && esThen(_structs[p].token))
                        {
                            p++;
                            if (p<_structs.Count && !esElse(_structs[p].token) && !esEnd(_structs[p].token))
                            {
                                do
                                {
                                    Sentencia();
                                } while (p < _structs.Count && (!esElse(_structs[p].token) && !esEnd(_structs[p].token)));
                            }
                            if (p < _structs.Count && esElse(_structs[p].token))
                            {

                                activaelse = true;
                                contadorIF--;
                                if (contadorIF < 0) contadorIF++;
                                _polish.Add(new ObjPolish
                                {
                                    operador = "BRI",
                                    resultado = "Q" + contadorIF
                                });
                                BusquedaQ("Q" + contadorIF);
                                p++;
                                if (p < _structs.Count && !esEnd(_structs[p].token))
                                {
                                    do
                                    {
                                        Sentencia();
                                    } while (p < _structs.Count && !esEnd(_structs[p].token));
                                }
                                
                            }
                            int aux = p;
                            while (p <= _structs.Count)
                            {
                                if (p < _structs.Count && esEnd(_structs[p].token))
                                {
                                    _polish.Add(new ObjPolish
                                    {
                                        apuntador = "Q" + contadorIF
                                    });
                                    break;
                                }
                                else 
                                {
                                    if (p == _structs.Count)
                                    {
                                        errores.Add(_error[0] + _structs[p-1].linea);
                                        break;
                                    }
                                }
                                p++;    
                            }
                            p = aux+1;
                              
                        }
                        else
                        {
                            errores.Add(_error[1] + _structs[p-1].linea);    
                        }
                    }
                }
                else if (esFor(_structs[p].token))
                {
                     p++;
                  
                     if (esIden(_structs[p].token))
                     {
                         ScheariD();
                         p++;
                        
                         if (esIgual(_structs[p].token))
                         {
                             p++;
                            
                            Metodoexp();
                            while (stackAnalisis.Count > 0)
                            {
                                queueSolucions.Enqueue(stackAnalisis.Pop());
                            }
                            AsignarValor();
                            //AsignarTipo(_idenuso, soluciStack.Pop());
                            soluciStack.Clear();
                            _idenuso.Clear(); 

                            if (esComa(_structs[p].token))
                             {
                                 p++;
                                 
                                 Metodoexp();
                                 if(soluciStack.Pop().tipo > 205)
                                 if (esDo(_structs[p].token))
                                 {
                                     p++;
                                    
                                     if (p < _structs.Count && esEnd(_structs[p].token))
                                     {
                                         _structs.RemoveAt(p);

                                     }
                                     else
                                     {
                                         Bloque();
                                         int aux = p;
                                         while (p <= _structs.Count)
                                         {
                                             if (p < _structs.Count && esEnd(_structs[p].token))
                                             {
                                                 _structs.RemoveAt(p);
                                                 break;
                                             }
                                             else
                                             {
                                                 if (p == _structs.Count)
                                                 {
                                                     errores.Add(_error[0] + _structs[aux-1].linea); 
                                                     break;
                                                 }
                                             }
                                             p++;
                                         }
                                         p = aux;
                                     }
                                     }
                                     else
                                     {
                                         errores.Add(_error[1] + _structs[p-1].linea);
                                     }
                            }
                         }
                         else
                         {
                             errores.Add(_error[2] + _structs[p-1].linea);
                         }
                     }
                     else
                     {
                         errores.Add(_error[3]+ _structs[p-1].linea);     
                     }
                }
                else if (esIden(_structs[p].token))
                {
                    ScheariD();
                    //_idenuso.Add(_structs[p].lexema);
                    queueSolucions.Enqueue(_structs[p]);
                        p++;
                        if (VarList())
                        {
                            if (p < _structs.Count && esIgual(_structs[p].token))
                            {
                                stackAnalisis.Push(_structs[p]);
                                p++;
                                Metodoexp();
                                while (stackAnalisis.Count>0)
                                {
                                    queueSolucions.Enqueue(stackAnalisis.Pop());
                                }
                                AsignarValor();
                                soluciStack.Clear();
                                _idenuso.Clear();;
                            }
                            else
                            {
                                errores.Add(_error[2] + _structs[p-1].linea);
                            }
                        }
                        else {
                            errores.Add(_error[3]+ _structs[p-1].linea);
                        }
                }
                else if (esPrint(_structs[p].token))
                {
                    stackAnalisis.Push(_structs[p]);
                    p++;
                    if (espaIzq(_structs[p].token))
                    {
                        do
                        {
                            p++;
                            if (Exp(_structs[p].token))
                            {
                                p++;
                            }
                            else
                            {
                                Metodoexp();
                                while (stackAnalisis.Count > 0)
                                {
                                    queueSolucions.Enqueue(stackAnalisis.Pop());
                                }
                                AsignarValor();
                                soluciStack.Clear();

                            }
                            
                        } while (p < _structs.Count && esComa(_structs[p].token));
                        if (p<_structs.Count && espaDer(_structs[p].token))
                        {
                            p++;  
                        }
                        else
                        {
                            errores.Add(_error[4] + _structs[p-1].linea); 
                        }
                    }
                    else
                    {
                        errores.Add(_error[4] + _structs[p-1].linea);
                    }

                }
                else
                {
                    errores.Add(_error[5] + _structs[p-1].linea); 
                    p++;
                }
            }      
        }

        private void BusquedaQ(string v)
        {
            for (int i = 0; i < _polish.Count; i++)
            {
                if (_polish[i].resultado == v)
                {
                    _polish[i].operador = "BRF";
                    _polish[i].operador1 = _polish[i - 1].resultado;
                    _polish[i].resultado = "P" + contadorIF;
                    break;
                }
            }
       }

        public bool VarList()
        {
            bool h=true;
            while (p < _structs.Count && esComa(_structs[p].token))
            {
                p++;
                if (p< _structs.Count && esIden(_structs[p].token))
                {
                    _idenuso.Add(_structs[p].lexema);
                    p++;
                }
                else
                {
                    h = false;
                    break;
                }
            } 
            return h;       
        }
              
        private bool Metodoexp(){
             if (p < _structs.Count && (Exp(_structs[p].token) || esIden(_structs[p].token) || espaIzq(_structs[p].token)))
             {
                if (espaIzq(_structs[p].token))
                {
                    AccionaRealizar();
                    Prefiexp();
                }
                else
                {
                    if (esIden(_structs[p].token))
                    {
                        ScheariD();
                        queueSolucions.Enqueue(_structs[p]);
                    }
                    p++;
                } 
                if (p < _structs.Count && operadorBin(_structs[p].token))
                {
                    if(_structs[p].token == 120)
                    {
                        p++;
                    }
                    else
                    {
                        p++;
                        Metodoexp();
                    }
                   
                }
                else
                {
                    return true;
                } 
            }
            else
            {
                errores.Add(_error[6]+ _structs[p-1].linea);      
                return false;
            }
            return true;      
        }

        private void Prefiexp(){
      
                p++;
                Metodoexp();
                int aux = p;
                while (p <= _structs.Count)
                {
                    if (p < _structs.Count && espaDer(_structs[p].token))
                    {
                        AccionaRealizar();
                        break;
                    }
                    else
                    {
                        if (p == _structs.Count)
                        {
                            errores.Add(_error[4] + _structs[aux - 1].linea);
                            break;
                        }
                    }
                    p++;
                }
                p = aux+1;
            }
  
        private void ScheariD()
        {
            bool encontrada = false;
            for (int i = 0; i < _identificadoreses.Count; i++)
            {
                if (_structs[p].lexema == _identificadoreses[i].lexema)
                {
                    _structs[p].tipo = _identificadoreses[i].tipo;
                    encontrada = true;
                    break;
                }
            }
            if (!encontrada)
            {
                _structs[p].setTipo(210);
                _identificadoreses.Add(new Identificadores { lexema = _structs[p].lexema, tipo = _structs[p].tipo, mascara = _structs[p].lexema + _controlvar});
                encontrada = false;
            }

        }
        #region Analisa y Resuelve operaciones aritmeticas 
        private void AccionaRealizar()
        {
            switch (_structs[p].lexema)
            {
                case "*":
                    Iteraciondivymult();
                    stackAnalisis.Push(_structs[p]);
                    break;
                case "/":
                    Iteraciondivymult();
                    stackAnalisis.Push(_structs[p]);
                    break;
                case "+":
                    Accionmasomenos(_structs[p].lexema);
                    break;
                case "-":
                    Accionmasomenos(_structs[p].lexema);
                    break;
                case ")":
                    AccionparDerecho();
                    break;
                case "(":
                    stackAnalisis.Push(_structs[p]);
                    break;
                case ">":
                    EsoperadorOrden(_structs[p].lexema);
                    break;
                case ">=":
                    EsoperadorOrden(_structs[p].lexema);
                    break;
                case "<":
                    EsoperadorOrden(_structs[p].lexema);
                    break;
                case "<=":
                    EsoperadorOrden(_structs[p].lexema);
                    break;
                case "==":
                    EsoperadorOrden(_structs[p].lexema);
                    break;
                case "and":
                    EsoperadorOrden(_structs[p].lexema);
                    break;
                case "or":
                    EsoperadorOrden(_structs[p].lexema);
                    break;
                case "..":
                    EsoperadorOrden(_structs[p].lexema);
                    break;
                case "=":
                    stackAnalisis.Push(_structs[p]);
                    break;
            }
        }
        private void EsoperadorOrden(string operador)
        {
           
            if (stackAnalisis.Count > 0 && stackAnalisis.Peek().lexema != "(" )
            {
                queueSolucions.Enqueue(stackAnalisis.Pop());
            }
            stackAnalisis.Push(_structs[p]);
        }
        protected void AccionparDerecho()
        {
            while (stackAnalisis.Count > 0 && stackAnalisis.Peek().lexema != "(")
            {
                queueSolucions.Enqueue(stackAnalisis.Pop());
            }
            stackAnalisis.Pop();
        }
        protected void Accionmasomenos(string c)
        {
            for (int i = 0; i < stackAnalisis.Count; i++)
            {
                Iteraciondivymult();
                if(stackAnalisis.Peek().lexema == "+" || stackAnalisis.Peek().lexema == "-")
                {
                    queueSolucions.Enqueue(stackAnalisis.Pop());
                }
            }
            
            stackAnalisis.Push(_structs[p]);

        }
        protected bool Iteraciondivymult()
        {
            bool v = false;
            if (stackAnalisis.Count>0 && (stackAnalisis.Peek().lexema == "*" || stackAnalisis.Peek().lexema == "/"))
            {
                queueSolucions.Enqueue(stackAnalisis.Pop());
                Iteraciondivymult();
                v = true;
            }
            return v;
        }

        bool asignacion = false;
        private void AsignarValor()
        {
            while (queueSolucions.Count >0)
            {
                    try
                    {
                        switch (queueSolucions.Peek().lexema)
                        {
                            case "*":
                                Operators("*");
                                break;
                            case "/":
                                Operators("/");
                                break;
                            case "+":
                                Operators("+");
                                break;
                            case "-":
                                Operators("-");
                                break;
                            case "..":
                                Concatenacion();
                                break;
                            case ">":
                                OperatorsRelacionales(">");
                                break;
                            case ">=":
                                OperatorsRelacionales(">=");
                                break;
                            case "<":
                                OperatorsRelacionales("<");
                                break;
                            case "<=":
                                OperatorsRelacionales("<=");
                                break;
                            case "==":
                                OperatorsRelacionales("==");
                                break;
                            case "and":
                                OperatorsLogic();
                                break;
                            case "or":
                                OperatorsLogic();
                                break;
                            case "=":
                                OperatorsAsignacion();
                                break;
                            case "print":
                                _polish.Add(new ObjPolish
                                {
                                    operador = "print",
                                    operador1 = soluciStack.Pop().lexema
                                });
                                queueSolucions.Dequeue();
                            break;
                        default:
                                soluciStack.Push(queueSolucions.Dequeue());
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                       
                    }
                    
                
            }
        }

        private void OperatorsAsignacion()
        {
            bool encontrada = false;
            queueSolucions.Dequeue();
            
            var valor2 = soluciStack.Pop();
            var valor1 = soluciStack.Pop();
            if (asignacion)
            {
                if (activaelse)
                {
                    _polish.Add(new ObjPolish
                    {
                        operador = "=",
                        operador1 = "T" + contador,
                        resultado = valor1.lexema,
                        apuntador = "P"+contadorIF
                    });
                    activaelse = false;
                }
                else
                {
                    _polish.Add(new ObjPolish
                    {
                        operador = "=",
                        operador1 = "T" + contador,
                        resultado = valor1.lexema
                    });
                }
                
                asignacion = false;
            }
            else
            {
                if (activaelse)
                {
                    _polish.Add(new ObjPolish
                    {
                        operador = "=",
                        operador1 = valor2.lexema,
                        resultado = valor1.lexema,
                        apuntador = "P"+contadorIF
                    });
                    activaelse = false;
                }
                else
                {
                    _polish.Add(new ObjPolish
                    {
                        operador = "=",
                        operador1 = valor2.lexema,
                        resultado = valor1.lexema
                    });

                }
                
            }
            for (int i = 0; i < _identificadoreses.Count; i++)
            {
                if(valor1.lexema == _identificadoreses[i].lexema)
                {
                    _identificadoreses[i].tipo = valor2.tipo;
                    encontrada = true;
                }
            }
            if (!encontrada)
            {
                _identificadoreses.Add(new Identificadores { lexema = valor1.lexema, tipo = valor2.tipo, mascara = valor1.lexema + _controlvar });
            }
        }

        private void OperatorsRelacionales(string op)
        {
            queueSolucions.Dequeue();

            var valor2 = soluciStack.Pop();
            var valor1 = soluciStack.Pop();

            if ((valor1.tipo == 101 ) && (valor2.tipo == 101))
            {
                contador++;
                _polish.Add(new ObjPolish
                {
                    operador = op,
                    operador1 = valor1.lexema,
                    operador2 = valor2.lexema,
                    resultado = "T" + contador,
                });
                valor1.lexema = "T" + contador;
                soluciStack.Push(valor1);
            }
            else
            {
                errores.Add("Error Semantica");
            }
        }

        private void OperatorsLogic()
        {
            queueSolucions.Dequeue();

            var valor2 = soluciStack.Pop();
            var valor1 = soluciStack.Pop();

            if ((valor1.tipo == 206 || valor1.tipo == 212 || valor1.tipo == 210) && (valor2.tipo == 206 || valor2.tipo == 212 || valor2.tipo == 210))
            {
                soluciStack.Push(valor1);
            }
            else
            {
                errores.Add("Error Semantica");
            }
        }

        private void Concatenacion()
        {
            queueSolucions.Dequeue();

            var valor2 = soluciStack.Pop();
            var valor1 = soluciStack.Pop();

            if ((valor1.tipo == 101 || valor1.tipo==103) && (valor2.tipo == 101 || valor2.tipo==103) )
            {
                soluciStack.Push(valor1);
            }
            else
            {
                errores.Add("Error Semantica");
            }
        }

        private void Operators(string op)
        {
            queueSolucions.Dequeue();
            
            var valor2 = soluciStack.Pop();
            var valor1 = soluciStack.Pop();
            
            if (valor1.tipo ==101 && valor2.tipo == 101)
            {
                contador++;
                if (activaelse)
                {
                    _polish.Add(new ObjPolish
                    {
                        operador = op,
                        operador1 = valor1.lexema,
                        operador2 = valor2.lexema,
                        resultado = "T" + contador,
                        apuntador = "P"+ contadorIF
                    });
                    activaelse = false;
                }
                else
                {
                    _polish.Add(new ObjPolish
                    {
                        operador = op,
                        operador1 = valor1.lexema,
                        operador2 = valor2.lexema,
                        resultado = "T" + contador,
                    });
                }
                
                asignacion = true;
                valor1.lexema = "T" + contador;
                soluciStack.Push(valor1);
            }
            else
            {
                errores.Add("Error Semantica");
            }
        }

        #endregion

        #region Busca Tokens

        private bool Exp(int token)
        {
           
        switch (token)
        {
            case 210:
                queueSolucions.Enqueue(_structs[p]);
                return true;
            case 215:
                queueSolucions.Enqueue(_structs[p]);
                return true;
            case 206:
                queueSolucions.Enqueue(_structs[p]);
                return true;
            case 101:
                queueSolucions.Enqueue(_structs[p]);
                return true;
            case 103:
                queueSolucions.Enqueue(_structs[p]);
                return true;
            default:
                return false;
            }
    }
        private bool operadorBin(int token){
            
            switch (token){
                case 108:
                    // *
                    AccionaRealizar();
                    return true;
                case 109:
                    // +
                    AccionaRealizar();
                    return true;
                case 104:
                    // -
                    AccionaRealizar();
                    return true;
                case 105:
                    AccionaRealizar();
                    return true;
                case 116:
                    AccionaRealizar();
                    return true;
                case 118:
                    AccionaRealizar();
                    return true;
                case 112:
                    return true;
                case 117:
                    AccionaRealizar();
                    return true;
                case 119:
                    AccionaRealizar();
                    return true;
                case 120:
                    AccionaRealizar();
                    return true;
                case 121:
                    AccionaRealizar();
                    return true;
                case 122:
                    return true;
                case 123:
                    // /
                    AccionaRealizar();
                    return true;
                case 201:
                    //and
                    AccionaRealizar();
                    return true;
                case 212:
                    //or
                    AccionaRealizar();
                    return true;  
                default:
                    return false;      
        }
    }
        
        #endregion
#region Verificaciones
        private bool esIf(int token)
    {
        if (token == 208)
        {
            return true;
        }
        return false;
    }

    private bool esThen(int token)
    {
        if (token == 214)
        {
            return true;
        }
        return false;
    }
    private bool esEnd(int token)
    {
        if (token == 205)
        {
            return true;
        }
        return false;
    }

    private bool esFor(int token)
    {
        if (token == 207)
        {
            return true;
        }
        return false;
    }

    private bool esIden(int token)
    {
        if (token == 102)
        {
            return true;
        }
        return false;
    }
    private bool esIgual(int token)
    {
        if (token == 120)
        {
            return true;

        }
        return false;
    }
    private bool esPrint(int token)
    {
        if (token == 216)
        {
            return true;
        }
        return false;
    }

    private bool esElse(int token)
    {
        if (token == 204)
        {
            return true;
        }
        return false;
    }

    private bool esComa(int token)
    {
        if (token == 125)
        {
            return true;
        }
        return false;

    }
    private bool esDo(int token)
    {
        if (token == 203)
        {
            return true;
        }
        return false;

    }
    private bool espaDer(int token)
    {
        if (token == 115)
        {
            return true;
        }
        return false;

    }
    private bool espaIzq(int token)
    {
        if (token == 114)
        {
            return true;
        }
        return false;
    }
}
#endregion
}