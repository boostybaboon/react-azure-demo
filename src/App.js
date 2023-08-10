import React, { useState, useEffect } from 'react';

function App() {
  const [data, setData] = useState('');

  useEffect(() => {
    (async function () {
      const { text } = await (await fetch(`/api/message`)).json();
      setData(text);
    })();
  });

  async function list() {

    const query = `
        {
          people {
            items {
              id
              Name
            }
          }
        }`;
        
    const endpoint = "/data-api/graphql";
    const response = await fetch(endpoint, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ query: query })
    });
    const result = await response.json();
    console.table(result.data.people.items);
  }

    async function list_from_api_using_graphql() {


    var res = await (await fetch(`/api/message3`)).json();

    console.log(res);

    //console.table(res.data.people.items);
  }

  function nothing(){
    console.log("nothing function called...");
  }

  return <div>
    <div>Text from front end!</div>
    <div>{data}</div>
    <div>
      <h1>Static Web Apps Database Connections</h1>
      <blockquote>
        Open the console in the browser developer tools to see the API responses.
      </blockquote>
      <div>
        <button id="list" onClick={list}>List</button>
        <button id="list from api using graphql" onClick={list_from_api_using_graphql}>List from api using graphql</button>
        <button id="get" onClick={nothing}>Get</button>
        <button id="update" onClick={nothing}>Update</button>
        <button id="create" onClick={nothing}>Create</button>
        <button id="delete" onClick={nothing}>Delete</button>
      </div>
    </div>
  </div>;
}

export default App;