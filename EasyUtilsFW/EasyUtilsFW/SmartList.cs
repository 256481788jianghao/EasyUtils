using System;

namespace EasyUtilsFW
{
    public class SmartList<T>
    {
        T[] m_Buffer;
        int m_BufferMaxLen;
        int m_CurEndIndex;
        SmartListState m_State;

        public enum SmartListState
        {
            IDEL,
            Inputing,
            Outputing
        }

        public SmartListState State { get { return m_State; } }
        public int Length { get { return (m_CurEndIndex + 1); } }

        public SmartList(int maxlen)
        {
            m_BufferMaxLen = maxlen;
            m_Buffer = new T[maxlen];
            m_CurEndIndex = -1;
            m_State = SmartListState.IDEL;
        }

        public void AppendAtEnd(T value)
        {
            AppendAtEnd(new T[] { value });
        }

        public void AppendAtEnd(T[] value_buffer,int len)
        {
            T[] input = new T[len];
            for(int i = 0; i < len; i++)
            {
                input[i] = value_buffer[i];
            }
            AppendAtEnd(input);
        }

        public void AppendAtEnd(T[] values)
        {
            if (m_State == SmartListState.IDEL)
            {
                if (Length+values.Length <= m_BufferMaxLen)
                {
                    m_State = SmartListState.Inputing;
                    foreach(T item in values)
                    {
                        m_CurEndIndex++;
                        m_Buffer[m_CurEndIndex] = item;
                    }
                    m_State = SmartListState.IDEL;
                }
                else
                {
                    m_State = SmartListState.IDEL;
                    throw new Exception("buffer is full");
                }
            }
            else
            {
                m_State = SmartListState.IDEL;
                throw new Exception("input at wrong state [" + m_State.ToString() + "]");
            }
        }

        public T[] OutputFromFirst(int len)
        {
            if(Length <= 0)
            {
                return null;
            }
            else if(len <= 0)
            {
                return null;
            }
            else
            {
                if(m_State == SmartListState.IDEL)
                {
                    m_State = SmartListState.Outputing;
                    T[] ret;
                    if (Length < len)
                    {
                        ret = OutputInternal(Length);
                        m_State = SmartListState.IDEL;
                        return ret;
                    }
                    else
                    {
                        ret = OutputInternal(len);
                        m_State = SmartListState.IDEL;
                        return ret;
                    }
                }
                else
                {
                    throw new Exception("output at wrong state [" + m_State.ToString() + "]");
                }
            }
        }

        private T[] OutputInternal(int len)
        {
            T[] ret = new T[len];
            for(int i = 0; i < len; i++)
            {
                ret[i] = m_Buffer[i];
            }
            int startIndex = len;
            int endIndex = m_CurEndIndex;
            m_CurEndIndex = -1;
            for (; startIndex <= endIndex; startIndex++)
            {
                m_CurEndIndex++;
                m_Buffer[m_CurEndIndex] = m_Buffer[startIndex];
            }
            return ret;
        }

        public override string ToString()
        {
            if(Length <= 0)
            {
                return "[]";
            }
            else
            {
                string ret = "";
                for (int i = 0; i < Length; i++)
                {
                    ret += m_Buffer[i].ToString() + ",";
                }
                return "[" + ret.Substring(0, ret.Length - 1) + "]";
            }
            
        }
    }
}
