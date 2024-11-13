namespace LinkedListsConsole
{
    public class BinaryTree<T>
    {
        public class Node
        {
            public T Data { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node(T data)
            {
                Data = data;
                Left = null;
                Right = null;
            }
        }

        public Node Root { get; private set; }

        public BinaryTree(T rootData)
        {
            Root = new Node(rootData);
        }

        public void AddLeft(Node parent, T data)
        {
            parent.Left = new Node(data);
        }

        public void AddRight(Node parent, T data)
        {
            parent.Right = new Node(data);
        }

        // Прямой обход (Префиксный)
        public static void PreOrderTraversal(Node node)
        {
            if (node != null)
            {
                Console.Write(node.Data + " ");

                // Префиксный обход: если у узла есть левый ребенок, обрабатываем его.
                if (node.Left != null)
                {
                    PreOrderTraversal(node.Left);
                }
                else if (node.Right != null) // Если правый ребенок существует, ставим '*' для отсутствующего левого.
                {
                    Console.Write("* ");
                }

                // Префиксный обход: если у узла есть правый ребенок, обрабатываем его.
                if (node.Right != null)
                {
                    PreOrderTraversal(node.Right);
                }
                else if (node.Left != null) // Если левый ребенок существует, ставим '*' для отсутствующего правого.
                {
                    Console.Write("* ");
                }
            }
        }


        // Симметричный обход (Инфиксный)
        public static void InOrderTraversal(Node node)
        {
            if (node == null)
            {
                Console.Write("* ");
                return;
            }

            // Инфиксный обход: если у узла есть левый ребенок, обрабатываем его.
            if (node.Left != null)
            {
                InOrderTraversal(node.Left);
            }
            else if (node.Right != null) // Если правый ребенок существует, ставим '*' для отсутствующего левого.
            {
                Console.Write("* ");
            }

            // Печатаем текущий узел
            Console.Write(node.Data + " ");

            // Инфиксный обход: если у узла есть правый ребенок, обрабатываем его.
            if (node.Right != null)
            {
                InOrderTraversal(node.Right);
            }
            else if (node.Left != null) // Если левый ребенок существует, ставим '*' для отсутствующего правого.
            {
                Console.Write("* ");
            }
        }



        // Обратный обход (Постфиксный)
        public static void PostOrderTraversal(Node node)
        {
            if (node == null)
            {
                Console.Write("* ");
                return;
            }

            // Постфиксный обход: если у узла есть левый ребенок, обрабатываем его.
            if (node.Left != null)
            {
                PostOrderTraversal(node.Left);
            }
            else if (node.Right != null) // Если правый ребенок существует, ставим '*' для отсутствующего левого.
            {
                Console.Write("* ");
            }

            // Постфиксный обход: если у узла есть правый ребенок, обрабатываем его.
            if (node.Right != null)
            {
                PostOrderTraversal(node.Right);
            }
            else if (node.Left != null) // Если левый ребенок существует, ставим '*' для отсутствующего правого.
            {
                Console.Write("* ");
            }

            // Печатаем текущий узел
            Console.Write(node.Data + " ");
        }


    }
}
