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
  </div>;
}

export default App;