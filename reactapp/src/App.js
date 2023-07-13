import { Box, Typography } from "@mui/material";
import Table from "./Table";
import { useEffect, useState } from "react";
import { getProducts } from "./service";
import TextField from "@mui/material/TextField";

// Show products table

export default function App() {
  const [products, setProducts] = useState([]);
  const [totalCount, setTotalCount] = useState(0);
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [search, setSearch] = useState("");

  useEffect(() => {
    getProducts({ page, pageSize, search }).then((data) => {
      setProducts([...products, ...data.productos]);
      setTotalCount(data.total);
    });
  }, [page, pageSize]);

  useEffect(() => {
    getProducts({ page, pageSize, search }).then((data) => {
      setProducts([...data.productos]);
      setPage(1);
      setTotalCount(data.total);
    });
  }, [search]);

  return (
    <Box sx={{ padding: 4 }}>
      <Typography
        color="skyblue"
        marginBottom={3}
        textAlign="center"
        variant="h2"
      >
        Productos
      </Typography>

      <TextField
        id="standard-basic"
        label="Buscar..."
        variant="standard"
        sx={{ marginBottom: 4 }}
        onChange={(e) => {
          setSearch(e.target.value);
        }}
      />
      <Table
        totalCount={totalCount}
        data={products}
        onPageChange={(page) => {
          setPage(page);
        }}
        onPageSizeChange={(pageSize) => {
          setPageSize(pageSize);
        }}
      />
    </Box>
  );
}
