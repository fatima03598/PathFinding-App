from read_path import create_connection, select_path
import requests
import os


url = 'http://localhost:8000/findPath'
myobj = {'source': 'Node6_6', 'target':'Node9_8'}

response = requests.post(url, data = myobj)

id = None
if "Invalid" in response.text:
    print(response.text)
    print("No Id")   
else:   
    id = int(response.text)
 

def main():
    path = "./Pathfinding.db"
    if not id:
        return
    # create a database connection
    conn = create_connection(path)
    with conn:
        select_path(conn, id)

        

if __name__ == '__main__':
    main()
