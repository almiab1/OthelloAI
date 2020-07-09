using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Tile[] board = new Tile[Constants.NumTiles]; // estado del tablero de este nodo
    public Node parent; // nodo padre
    public List<Node> childList = new List<Node>(); // lista de nodos hijos
    public int type;//Constants.MIN o Constants.MAX
    public double utility; // Define la utilidad del nodo
    public double alfa; // Componente alfa del algoritmo
    public double beta; // Componente beta del algoritmo
    public int tileToMove; // Ficha que se movera

    // clase nodo
    public Node(Tile[] tiles)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            this.board[i] = new Tile();
            this.board[i].value = tiles[i].value;
        }

    }

}

public class Player : MonoBehaviour
{
    public int initialTurn;
    public int turn;
    private BoardManager boardManager;

    private float infinito = Mathf.Infinity;
    private float menosInfinito = Mathf.NegativeInfinity;

    void Start()
    {
        boardManager = GameObject.FindGameObjectWithTag("BoardManager").GetComponent<BoardManager>();
    }

    /*
     * Entrada: Dado un tablero
     * Salida: Posición donde mueve  
     */
    public int SelectTile(Tile[] board)
    {
        initialTurn = turn;

        //Generamos el nodo raíz del árbol (MAX)
        Node root = new Node(board);
        root.type = Constants.MAX;

        //Generamos primer nivel de nodos hijos
        //List<int> selectableTiles = boardManager.FindSelectableTiles(board, turn);

        generacionArbol(root,4);
        minimax(root, 4, menosInfinito, infinito);

        foreach (var child in root.childList)
        {
            if (root.utility == child.utility)
            {
                root.tileToMove = child.tileToMove;
            }
        }

        turn = initialTurn;
        return root.tileToMove;

    }

    private void generacionArbol(Node root, int depth)
    {
        if(depth > 0)
        {
            generarHijos(root.board, root);
            foreach (var child in root.childList)
            {
                generacionArbol(child, depth - 1);
            }
        }
    }
    /*
     * Calcula la utiliadad de un nodo y anade la tile optima al parametro del nodo 'tileToMove'
    */
    private double calcularUtilidadNodo(Node nodo)
    {
        double utility = nodo.utility; // Utilidad que se devolvera
        List<int> selectableTiles = boardManager.FindSelectableTiles(nodo.board, -nodo.type); // Fichas donde podra moverse el nodo

        foreach (var selected in selectableTiles)
        {
            int newUtility = calcularUtilidadTile(nodo.board, nodo.type, selected);
            switch (nodo.type)
            {
                // Caso en el que el nodo es MIN
                case Constants.MIN:
                    if (utility > newUtility)
                    {
                        utility = newUtility;
                    };
                    break;

                case Constants.MAX:
                    if (utility < newUtility)
                    {
                        utility = newUtility;
                    };
                    break;

                default:
                    break;
            }
        }

        return utility;

    }

    /*
     * Calcula la utilidad de un tile
    */
    private int calcularUtilidadTile(Tile[] board, int turn, int selectedTile)
    {
        List<int> swappableTiles = boardManager.FindSwappablePieces(board, selectedTile, -turn);

        int utility = swappableTiles.Count;

        return utility;
    }

    private void generarHijos(Tile[] board, Node parent)
    {
        List<int> selectableTiles = boardManager.FindSelectableTiles(board, turn);

        foreach (int s in selectableTiles)
        {
            //Creo un nuevo nodo hijo con el tablero padre
            Node n = new Node(parent.board);
            //Lo anadimos a la lista de nodos hijo
            parent.childList.Add(n);
            //Enlazo con su padre
            n.parent = parent;
            //Dependiendo del nivel del padre ponemos un tipo u otro 
            if (parent.type == Constants.MIN)
            {
                n.type = Constants.MAX;
            }
            else
            {
                n.type = Constants.MIN;
            }

            n.tileToMove = s;
            //Aplico un movimiento, generando un nuevo tablero con ese movimiento
            boardManager.Move(n.board, s, turn);
            //si queremos imprimir el nodo generado (tablero hijo)
            //boardManager.PrintBoard(n.board);
        }

        turn = -turn;
    }

    private double minimax(Node node, int depth, double alpha, double beta)
    {
        if (depth == 0)
        {
            node.utility = calcularUtilidadNodo(node);

            return calcularUtilidadNodo(node);
        }

        if (node.type == Constants.MAX)
        {

            double maxEval = menosInfinito;

            foreach (var child in node.childList)
            {
                double eval = minimax(child, depth - 1, alpha, beta);

                maxEval = Math.Max(maxEval, eval);

                alpha = Math.Max(alpha, eval);

                if (beta <= alpha)
                {
                    break;
                }
            }

            return maxEval;
        }

        else
        {

            double minEval = infinito;

            foreach (var child in node.childList)
            {
                double eval = minimax(child, depth - 1, alpha, beta);

                minEval = Math.Min(minEval, eval);

                beta = Math.Min(beta, eval);

                if (beta <= alpha)
                {
                    break;
                }
            }

            return minEval;
        }
    }
}
