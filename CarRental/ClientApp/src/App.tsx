import * as React from 'react';
import './custom.css'
import CarTable from './components/CarsTable';
import AdvancedSearch from './components/AdvancedSearch';
import { Typography } from '@mui/material';

//Main Component
export default () => {

    return (
        <>
            <Typography variant="h2" style={{ margin: '32px', textAlign: 'center' }}>Car Rental</Typography>
            <AdvancedSearch />
            <CarTable />
        </>
    );
}
