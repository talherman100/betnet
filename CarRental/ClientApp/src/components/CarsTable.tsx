import React, { useEffect, useState } from 'react';
import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper } from '@mui/material';
import axios from 'axios';
import { useAppSelector } from '../store/DataStore';
import useDebounce from '../hooks/useDebounce ';

interface Car {
    carId: number;
    description: string;
    price: number;
    discounts: number;
    minimumDriverAge: number;
    location: string;
    availableExtras: string;
    carGroup: any;
}

const CarTable: React.FC = () => {

    const [carData, setCarData] = useState<Car[]>([]);
    const advancedSearchParams = useAppSelector(dataStore => dataStore.advancedSearchParams);

    //Debouncer to reduce frequent calls to the server
    const debouncedAdvancedSearch = useDebounce(advancedSearchParams, 1000);

    const fetchData = () => {
        axios.post('/api/carrental/get-filtered-cars', debouncedAdvancedSearch)
            .then(response => {
                setCarData(response.data);
            })
            .catch(error => {
                console.error('Error fetching data:', error);
            });
    }
    useEffect(() => {
        if (debouncedAdvancedSearch) {
            fetchData();
        }
    }, [debouncedAdvancedSearch]);

    return (
        <TableContainer component={Paper}>
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell>ID</TableCell>
                        <TableCell>Description</TableCell>
                        <TableCell>Price</TableCell>
                        <TableCell>Discounts</TableCell>
                        <TableCell>Minimum Driver Age</TableCell>
                        <TableCell>Location</TableCell>
                        <TableCell>Available Extras</TableCell>
                        <TableCell>Car Group</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {carData.map(car => (
                        <TableRow key={car.carId}>
                            <TableCell>{car.carId}</TableCell>
                            <TableCell>{car.description}</TableCell>
                            <TableCell>${car.price}</TableCell>
                            <TableCell>${car.discounts}</TableCell>
                            <TableCell>{car.minimumDriverAge}</TableCell>
                            <TableCell>{car.location}</TableCell>
                            <TableCell>{car.availableExtras}</TableCell>
                            <TableCell>{car.carGroup.name}</TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
};

export default CarTable;
