import sqlite3
from sqlite3 import Error

def create_connection(db_file):
    conn = None
    try:
        conn = sqlite3.connect(db_file)
    except Error as e:
        print(e)

    return conn

def select_path(conn, id):
   
    cur = conn.cursor()
    cur.execute("SELECT * FROM paths WHERE id=?", (id,))

    rows = cur.fetchall()

    for row in rows:
        print(f"source:{row[1]}    target:{row[2]}    distance:{row[3]} \n\n  path: {row[4]}")