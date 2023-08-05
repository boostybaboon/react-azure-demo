import React, { useState, useEffect } from 'react';

function App() {
  const [data, setData] = useState('');

  useEffect(() => {
    (async function () {
      const { text } = await (await fetch(`/api/message`)).json();
      setData(text);
    })();
  });

  return <div>
    <div>Text from front end!</div>
    <div>{data}</div>
    <div>
      <h1>Static Web Apps Database Connections</h1>
      <blockquote>
        Open the console in the browser developer tools to see the API responses.
      </blockquote>
      <div>
        <button id="list" onclick="list()">List</button>
        <button id="get" onclick="get()">Get</button>
        <button id="update" onclick="update()">Update</button>
        <button id="create" onclick="create()">Create</button>
        <button id="delete" onclick="del()">Delete</button>
      </div>
    </div>
  </div>;
}

export default App;