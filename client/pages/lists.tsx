const Lists = ({ lists }) => {
  return (
    <>
      <h1>Your Lists</h1>
      {lists.map((list) => {
        return <h3>{list.name}</h3>;
      })}
    </>
  );
};

export async function getServerSideProps(context) {
  process.env.NODE_TLS_REJECT_UNAUTHORIZED = "0";
  const res = await fetch(`https://localhost:5001/InventoryList`);
  const data = await res.json();
  
  return {
    props: { lists: data },
  };
}

export default Lists;
